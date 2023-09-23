const Common = {
    async ajax({ method, url, data } = {}) {
        try {
            const request = { method, url, data };
            const response = await axios(request);
            return response.data;
        }
        catch (err) {
            console.log(err.message);
            return undefined;
        }
    },

    getUrl(url) {
        return `${window.location.protocol}//${window.location.host}${url}`;
    },

    renderClassValidationForm(btnId) {
        const btn = document.querySelector(`#${btnId}`);
        const inputs = document.querySelectorAll('.form-control');
        const events = ['keyup', 'blur'];

        btn.addEventListener('click', () => {
            setTimeout(() => {
                const invalidInputs = document.querySelectorAll('.input-validation-error');
                const validInputs = document.querySelectorAll('.valid');

                invalidInputs.forEach((input) => {
                    Common.renderInvalidInput(input);
                });

                validInputs.forEach((input) => {
                    Common.renderValidInput(input);
                });
            })
        })

        events.forEach((event) => {
            inputs.forEach((input) => {
                input.addEventListener(event, (e) => {
                    setTimeout(() => {
                        if (e.target.classList.contains('input-validation-error')) {
                            Common.renderInvalidInput(e.target);
                        }

                        if (e.target.classList.contains('valid')) {
                            Common.renderValidInput(e.target);
                        }
                    });
                });
            });
        });
    },

    renderValidInput(input) {
        input.classList.remove('is-invalid');
        input.classList.add('is-valid');
    },

    renderInvalidInput(input) {
        input.classList.add('is-invalid');
        input.classList.remove('is-valid');
    },
};

(function () {
    const alertElement = document.querySelector('.alert');

    if (!alertElement) {
        return;
    }

    setTimeout(() => {
        alertElement.classList.add("d-none");
    }, 2000)
})()