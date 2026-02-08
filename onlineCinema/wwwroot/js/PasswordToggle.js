function initPasswordToggle(buttonId, inputId, iconId) {
    const button = document.getElementById(buttonId);
    if (!button) return;

    button.addEventListener('click', function () {
        const input = document.getElementById(inputId);
        const icon = document.getElementById(iconId);

        if (input.type === 'password') {
            input.type = 'text';
            icon.classList.replace('bi-eye', 'bi-eye-slash');
        } else {
            input.type = 'password';
            icon.classList.replace('bi-eye-slash', 'bi-eye');
        }
    });
}