window.floatingPlayer = (() => {
    function init(rootEl) {
        if (!rootEl) return;

        const saved = localStorage.getItem("floatingPlayer.pos");
        if (saved) {
            try {
                const p = JSON.parse(saved);
                if (typeof p.left === "number") rootEl.style.left = p.left + "px";
                if (typeof p.top === "number") rootEl.style.top = p.top + "px";
            } catch { }
        }

        let dragging = false;
        let startX = 0, startY = 0;
        let startLeft = 0, startTop = 0;

        const handle = rootEl.querySelector(".fp-header") || rootEl;

        const onPointerDown = (e) => {
            dragging = true;
            rootEl.setPointerCapture?.(e.pointerId);

            const rect = rootEl.getBoundingClientRect();
            startX = e.clientX;
            startY = e.clientY;
            startLeft = rect.left;
            startTop = rect.top;

            rootEl.classList.add("dragging");
            e.preventDefault();
        };

        const onPointerMove = (e) => {
            if (!dragging) return;

            const dx = e.clientX - startX;
            const dy = e.clientY - startY;

            let left = startLeft + dx;
            let top = startTop + dy;

            const maxLeft = window.innerWidth - 40;
            const maxTop = window.innerHeight - 40;
            left = Math.max(-20, Math.min(left, maxLeft));
            top = Math.max(-20, Math.min(top, maxTop));

            rootEl.style.left = left + "px";
            rootEl.style.top = top + "px";

            localStorage.setItem("floatingPlayer.pos", JSON.stringify({ left, top }));
        };

        const onPointerUp = (e) => {
            if (!dragging) return;
            dragging = false;
            rootEl.classList.remove("dragging");
            e.preventDefault();
        };

        handle.style.touchAction = "none";
        handle.addEventListener("pointerdown", onPointerDown);
        window.addEventListener("pointermove", onPointerMove);
        window.addEventListener("pointerup", onPointerUp);
    }

    async function play(audioEl) {
        if (!audioEl) return { ok: false, reason: "no-audio" };
        try {
            await audioEl.play();
            return { ok: true };
        } catch (e) {
            return { ok: false, reason: e?.name ?? "play-failed" };
        }
    }

    function pause(audioEl) {
        if (audioEl) audioEl.pause();
    }

    function setSrc(audioEl, src) {
        if (!audioEl) return;
        audioEl.src = src;
        audioEl.load();
    }

    function getProgress(audioEl) {
        if (!audioEl) return { currentTime: 0, duration: 0 };
        return {
            currentTime: audioEl.currentTime || 0,
            duration: audioEl.duration || 0
        };
    }

    function seekBySlider(audioEl, slider0to1000) {
        if (!audioEl) return;
        const dur = audioEl.duration || 0;
        if (dur <= 0) return;
        audioEl.currentTime = (slider0to1000 / 1000.0) * dur;
    }

    return { init, play, pause, setSrc, getProgress, seekBySlider };
})();
