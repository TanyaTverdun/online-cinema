document.addEventListener('DOMContentLoaded', function ()
{
    const seats = document.querySelectorAll('.seat:not(.seat-booked)');
    const priceDisplay = document.getElementById('priceDisplay');
    const submitBtn = document.getElementById('submitBtn');
    const hiddenContainer = document.getElementById('selectedSeatsContainer');
    const summaryList = document.getElementById('selectedSeatsList');
    const emptyMessage = '<p id="empty-msg" class="small fst-italic" style="color: var(--bs-gray-600)">Select seats to proceed...</p>';

    let totalCount = 0;
    let totalPrice = 0;

    seats.forEach(seat => {
        seat.addEventListener('click', function ()
        {
            this.classList.toggle('seat-selected');
            const id = this.dataset.id;
            const price = parseFloat(this.dataset.price);
            const row = this.dataset.row;
            const num = this.dataset.num;
            const isVip = this.classList.contains('seat-vip');
            const seatType = isVip ? "Premium" : "Standard";

            if (this.classList.contains('seat-selected'))
            {
                totalCount++;
                totalPrice += price;
                document.getElementById('empty-msg')?.remove();

                const itemHtml = `
                    <div id="ui-item-${id}" class="d-flex justify-content-between align-items-center mb-3">
                        <div>
                            <span class="fw-bold" style="color: var(--bs-gray-200)">R ${row}, S ${num}</span>
                            <br><small style="color: var(--bs-gray-600); font-size: 0.75em; text-transform: uppercase;">${seatType}</small>
                        </div>
                        <span class="fw-bold" style="color: var(--bs-white)">$${price.toFixed(2)}</span>
                    </div>`;
                summaryList.insertAdjacentHTML('beforeend', itemHtml);

                const input = document.createElement('input');
                input.type = 'hidden';
                input.name = 'selectedSeatIds';
                input.value = id;
                input.id = 'input-seat-' + id;
                hiddenContainer.appendChild(input);
            } else
            {
                totalCount--;
                totalPrice -= price;
                document.getElementById(`ui-item-${id}`)?.remove();
                document.getElementById('input-seat-' + id)?.remove();
                if (totalCount === 0)
                {
                    summaryList.innerHTML = emptyMessage;
                }
            }
            priceDisplay.textContent = totalPrice.toFixed(2);
            submitBtn.disabled = totalCount === 0;
        });
    });
});