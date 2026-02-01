document.addEventListener('DOMContentLoaded', function () {
    const pageContainer = document.getElementById('snackSelectionPage');
    if (!pageContainer)
    {
        return;
    }

    const lockUntilStr = pageContainer.dataset.lockUntil;
    const baseSeatsPrice = parseFloat(pageContainer.dataset.seatsPrice.replace(',', '.'));

    // --- ЛОГІКА ТАЙМЕРА ---
    const countdownElement = document.getElementById("countdown");
    if (lockUntilStr && countdownElement) 
    {
        const lockUntil = new Date(lockUntilStr).getTime();
        const timerInterval = setInterval(function ()
        {
            const now = new Date().getTime();
            const distance = lockUntil - now;

            if (distance < 0)
            {
                clearInterval(timerInterval);
                countdownElement.innerHTML = "00:00";
                alert("Reservation time expired!");
                window.location.href = "/";
                return;
            }

            const minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
            const seconds = Math.floor((distance % (1000 * 60)) / 1000);

            countdownElement.innerHTML =
                (minutes < 10 ? "0" : "") + minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
        }, 1000);
    }

    // --- ЛОГІКА СНЕКІВ ---
    window.updateQty = function (btn, delta)
    {
        const group = btn.closest('.qty-group');
        const input = group.querySelector('.qty-input');
        let newVal = parseInt(input.value) + delta;
        if (newVal < 0)
        {
            newVal = 0;
        }

        input.value = newVal;

        const card = btn.closest('.snack-card');
        if (newVal > 0)
        {
            card.style.borderColor = "var(--bs-white)";
            card.style.backgroundColor = "#222";
        } else
        {
            card.style.borderColor = "var(--accent-border)";
            card.style.backgroundColor = "var(--card-bg)";
        }

        calculateGrandTotal();
    }

    function calculateGrandTotal()
    {
        let snacksTotal = 0;
        document.querySelectorAll('.snack-card').forEach(card => {
            const priceField = card.querySelector('.snack-price');
            const qtyField = card.querySelector('.qty-input');
            if (priceField && qtyField)
            {
                const price = parseFloat(priceField.value.replace(',', '.'));
                const qty = parseInt(qtyField.value);
                if (!isNaN(price) && !isNaN(qty))
                {
                    snacksTotal += price * qty;
                }
            }
        });

        const grandTotal = baseSeatsPrice + snacksTotal;
        document.getElementById('grandTotalDisplay').innerText = grandTotal.toFixed(2);
    }
});