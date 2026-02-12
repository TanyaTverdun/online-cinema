$(document).ready(function () {
    const $movieSelect = $('#MovieSelector');
    const $hallSelect = $('#HallSelector');

    if (!$movieSelect.length || !$hallSelect.length) return;

    // Отримуємо початковий ID залу з атрибута (якщо ми в режимі редагування)
    const initialHallId = $hallSelect.attr('data-initial-id');

    // Клонуємо всі опції залів, які прийшли з сервера
    const $allHallOptions = $hallSelect.find('option:not([value=""])').clone();

    function filterHalls(isInitialLoad = false) {
        const selectedOption = $movieSelect.find(':selected');
        const movieFeatsRaw = selectedOption.data('features');

        // Перетворюємо рядок ID у масив чисел
        const requiredFeatures = movieFeatsRaw ?
            String(movieFeatsRaw).split(',').filter(x => x).map(Number) : [];

        // Визначаємо, яке значення спробувати встановити
        const currentVal = isInitialLoad ? initialHallId : $hallSelect.val();

        // Очищаємо поточний список залів
        $hallSelect.find('option:not([value=""])').remove();

        $allHallOptions.each(function () {
            const $hallOpt = $(this);
            const hallFeatsRaw = $hallOpt.data('features');
            const hallFeatures = hallFeatsRaw ?
                String(hallFeatsRaw).split(',').filter(x => x).map(Number) : [];

            // Перевірка: чи містить зал всі фічі фільму?
            const isCompatible = requiredFeatures.every(f => hallFeatures.includes(f));

            if (isCompatible) {
                $hallSelect.append($hallOpt.clone());
            }
        });

        // Повертаємо виділення, якщо зал залишився в списку
        if ($hallSelect.find(`option[value="${currentVal}"]`).length > 0) {
            $hallSelect.val(currentVal);
        } else {
            $hallSelect.val("");
        }
    }

    $movieSelect.on('change', function () {
        filterHalls(false);
    });

    // Запуск при завантаженні
    if ($movieSelect.val()) {
        filterHalls(true);
    }
});