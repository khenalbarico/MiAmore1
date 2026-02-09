namespace LogicLbrary1.SmtpHandler1;
public static class MessageBody1
{
    public static string ReturnCustomizedBody(string herName = "Althea Joy Austria")
    {
        return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='UTF-8'>
                    <style>
                        body {{
                            font-family: 'Segoe UI', Arial, sans-serif;
                            background-color: #fff5f8;
                            color: #333;
                            padding: 20px;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: auto;
                            background-color: #ffffff;
                            border-radius: 14px;
                            padding: 28px;
                            box-shadow: 0 6px 15px rgba(0,0,0,0.12);
                        }}
                        .header {{
                            text-align: center;
                            color: #d6336c;
                        }}
                        .header h1 {{
                            margin-bottom: 6px;
                        }}
                        .divider {{
                            border-top: 1px solid #f3c2d4;
                            margin: 22px 0;
                        }}
                        .activity {{
                            margin-bottom: 18px;
                        }}
                        .activity-title {{
                            font-weight: bold;
                            color: #b02a57;
                        }}
                        .footer {{
                            margin-top: 35px;
                            text-align: center;
                            font-style: italic;
                            color: #777;
                        }}
                        .heart {{
                            color: #e63946;
                            font-size: 22px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h1>Will You Be My Valentine? 💘</h1>
                            <p>February 15, 2026 <span class='heart'>♥</span></p>
                        </div>

                        <div class='divider'></div>

                        <p>
                            Hi my love, <strong>{herName}</strong> 💕
                            <br /><br />
                            Every moment with you feels special, and I honestly can’t imagine
                            celebrating love without you by my side. You make my days brighter,
                            my smiles wider, and my heart fuller. 💖
                        </p>

                        <p>
                            So I planned a little day for us — just you and me —
                            filled with fun, laughter, and moments I’ll never forget. 🥰
                        </p>

                        <div class='divider'></div>

                        <div class='activity'>
                            <p class='activity-title'>🎬 Activity: Studio</p>
                            <p>📍 Where: SM Sangandaan</p>
                            <p>🕚 When: February 15, 2026 — 11:00 AM</p>
                        </div>

                        <div class='activity'>
                            <p class='activity-title'>⛸️ Activity: Ice Skating</p>
                            <p>📍 Where: SM Mall of Asia</p>
                            <p>🕑 When: February 15, 2026 — 2:00 PM to 4:00 PM</p>
                        </div>

                        <div class='activity'>
                            <p class='activity-title'>🍽️ Activity: Dinner Date</p>
                            <p>📍 Where: SM Mall of Asia</p>
                            <p>🕕 When: February 15, 2026 — 6:00 PM onwards</p>
                        </div>

                        <div class='divider'></div>

                        <p>
                            No matter where we go or what we do, my favorite part will always be
                            being with you. Holding your hand, hearing your laugh,
                            and sharing moments that mean everything to me. 💑
                            <br /><br />
                            So here I am, asking you sincerely and from the bottom of my heart…
                        </p>

                        <p style='text-align:center; font-size:18px;'>
                            <strong>{herName}, will you be my Valentine? 💖🌹</strong>
                        </p>

                        <div class='footer'>
                            <p>
                                Always yours,<br />
                                <strong>Your Valentine 💌</strong>
                            </p>
                        </div>
                    </div>
                </body>
                </html>";
                    }
}
