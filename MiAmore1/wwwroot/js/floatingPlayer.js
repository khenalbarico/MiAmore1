window.floatingPlayer = (function () {
    function getRoot(el) {
        return el && el.closest ? el.closest(".floating-player") : null;
    }

    function clamp01(n) {
        n = Number(n);
        if (!isFinite(n)) return 1;
        if (n < 0) return 0;
        if (n > 1) return 1;
        return n;
    }

    function clampInt(n, min, max) {
        n = parseInt(n, 10);
        if (!isFinite(n)) return min;
        if (n < min) return min;
        if (n > max) return max;
        return n;
    }

    function fmtReason(e) {
        if (!e) return "play-failed";
        if (typeof e === "string") return e;
        if (e.name) return e.name;
        if (e.message) return e.message;
        return "play-failed";
    }

    function supportsPassive() {
        let ok = false;
        try {
            const opts = Object.defineProperty({}, "passive", {
                get() {
                    ok = true;
                    return true;
                }
            });
            window.addEventListener("test-passive", null, opts);
            window.removeEventListener("test-passive", null, opts);
        } catch { }
        return ok;
    }

    function dragInit(root) {
        if (!root || root.__fpDragInit) return;
        root.__fpDragInit = true;

        const header = root.querySelector(".fp-header");
        if (!header) return;

        let startX = 0;
        let startY = 0;
        let baseLeft = 0;
        let baseTop = 0;
        let dragging = false;
        let pointerId = null;

        function getBase() {
            const rect = root.getBoundingClientRect();
            const cs = getComputedStyle(root);
            const left = parseFloat(cs.left);
            const top = parseFloat(cs.top);

            const hasTop = cs.top !== "auto" && !isNaN(top);
            const hasLeft = cs.left !== "auto" && !isNaN(left);

            const currentLeft = hasLeft ? left : rect.left;
            const currentTop = hasTop ? top : rect.top;

            return { left: currentLeft, top: currentTop, rect };
        }

        function constrain(left, top) {
            const rect = root.getBoundingClientRect();
            const w = rect.width;
            const h = rect.height;

            const maxLeft = Math.max(0, window.innerWidth - w - 8);
            const maxTop = Math.max(0, window.innerHeight - h - 8);

            if (left < 8) left = 8;
            if (top < 8) top = 8;
            if (left > maxLeft) left = maxLeft;
            if (top > maxTop) top = maxTop;

            return { left, top };
        }

        function onDown(e) {
            if (e.button !== undefined && e.button !== 0) return;

            const base = getBase();
            baseLeft = base.left;
            baseTop = base.top;

            startX = e.clientX;
            startY = e.clientY;

            dragging = true;
            pointerId = e.pointerId;

            root.classList.add("dragging");
            root.style.left = baseLeft + "px";
            root.style.top = baseTop + "px";
            root.style.bottom = "auto";
            root.style.right = "auto";

            try { header.setPointerCapture(pointerId); } catch { }

            e.preventDefault();
        }

        function onMove(e) {
            if (!dragging) return;
            if (pointerId !== null && e.pointerId !== pointerId) return;

            const dx = e.clientX - startX;
            const dy = e.clientY - startY;

            let left = baseLeft + dx;
            let top = baseTop + dy;

            const c = constrain(left, top);
            root.style.left = c.left + "px";
            root.style.top = c.top + "px";
        }

        function endDrag(e) {
            if (!dragging) return;
            if (pointerId !== null && e.pointerId !== pointerId) return;

            dragging = false;
            root.classList.remove("dragging");

            try { header.releasePointerCapture(pointerId); } catch { }
            pointerId = null;
        }

        const opt = supportsPassive() ? { passive: false } : false;

        header.addEventListener("pointerdown", onDown, opt);
        window.addEventListener("pointermove", onMove, opt);
        window.addEventListener("pointerup", endDrag, opt);
        window.addEventListener("pointercancel", endDrag, opt);
    }

    async function play(audio) {
        if (!audio) return { ok: false, reason: "no-audio" };
        try {
            const p = audio.play();
            if (p && typeof p.then === "function") await p;
            return { ok: true, reason: null };
        } catch (e) {
            return { ok: false, reason: fmtReason(e) };
        }
    }

    function pause(audio) {
        if (!audio) return;
        audio.pause();
    }

    function setMuted(audio, muted) {
        if (!audio) return;
        audio.muted = !!muted;
    }

    function setVolume(audio, volume01) {
        if (!audio) return;
        audio.volume = clamp01(volume01);
    }

    function setSrc(audio, src) {
        if (!audio) return;
        const next = src || "";
        const cur = audio.getAttribute("src") || "";
        if (cur !== next) audio.setAttribute("src", next);
        try { audio.load(); } catch { }
    }

    function seekBySlider(audio, slider) {
        if (!audio) return;
        const d = audio.duration;
        if (!d || !isFinite(d) || d <= 0) return;
        const v = clampInt(slider, 0, 1000);
        audio.currentTime = (v / 1000) * d;
    }

    function getProgress(audio) {
        if (!audio) return { currentTime: 0, duration: 0 };
        const d = audio.duration;
        return {
            currentTime: audio.currentTime || 0,
            duration: d && isFinite(d) ? d : 0
        };
    }

    function unmuteOnFirstGesture(audio, src) {
        if (!audio) return;
        if (audio.__fpUnmuteHooked) return;
        audio.__fpUnmuteHooked = true;

        const handler = async () => {
            try {
                if (src) {
                    const cur = audio.getAttribute("src") || "";
                    if (cur !== src) {
                        audio.setAttribute("src", src);
                        try { audio.load(); } catch { }
                    }
                }
                audio.muted = false;
                await play(audio);
            } catch { }
            window.removeEventListener("pointerdown", handler, true);
            window.removeEventListener("keydown", handler, true);
            window.removeEventListener("touchstart", handler, true);
        };

        window.addEventListener("pointerdown", handler, true);
        window.addEventListener("keydown", handler, true);
        window.addEventListener("touchstart", handler, true);
    }

    function init(root) {
        if (!root) return;
        dragInit(root);
    }

    return {
        init,
        setSrc,
        play,
        pause,
        setMuted,
        setVolume,
        seekBySlider,
        getProgress,
        unmuteOnFirstGesture
    };
})();
