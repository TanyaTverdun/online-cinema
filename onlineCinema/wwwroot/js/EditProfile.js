document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById('profileForm');
    if (!form) return;

    const actionButtons = document.getElementById('action-buttons');
    const cancelButton = document.getElementById('cancel-changes');
    const inputs = form.querySelectorAll('input:not([type="hidden"])');

    const initialValues = {};

    inputs.forEach(input => {
        initialValues[input.name] = input.value;
    });

    // Показуємо/ховаємо кнопки при зміні
    function checkForChanges() {
        let hasChanges = false;
        inputs.forEach(input => {
            if (input.value !== initialValues[input.name]) {
                hasChanges = true;
            }
        });

        if (hasChanges) {
            actionButtons.classList.remove('d-none');
        } else {
            actionButtons.classList.add('d-none');
        }
    }

    inputs.forEach(input => {
        input.addEventListener('input', checkForChanges);
        input.addEventListener('change', checkForChanges);
    });

    // повернення значень при скасуванні
    cancelButton.addEventListener('click', function () {
        inputs.forEach(input => {
            input.value = initialValues[input.name];
        });
        actionButtons.classList.add('d-none');
    });
});