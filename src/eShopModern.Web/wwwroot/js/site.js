// eShop Modern JavaScript

// Initialize when DOM is ready
document.addEventListener('DOMContentLoaded', function() {
    // Initialize Bootstrap tooltips
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Initialize Bootstrap popovers
    var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
    var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl);
    });

    // Form validation enhancement
    enhanceFormValidation();

    // Initialize delete confirmations
    initializeDeleteConfirmations();
});

// Enhance form validation with better UX
function enhanceFormValidation() {
    const forms = document.querySelectorAll('form[data-validation="true"]');

    forms.forEach(form => {
        const inputs = form.querySelectorAll('input, select, textarea');

        inputs.forEach(input => {
            // Real-time validation feedback
            input.addEventListener('blur', function() {
                validateField(this);
            });

            // Clear validation on input
            input.addEventListener('input', function() {
                clearFieldValidation(this);
            });
        });
    });
}

// Validate individual field
function validateField(field) {
    const validationSpan = field.parentElement.querySelector('.field-validation-error');

    if (field.validity && !field.validity.valid) {
        field.classList.add('input-validation-error');
        if (validationSpan) {
            validationSpan.style.display = 'block';
        }
    } else {
        field.classList.remove('input-validation-error');
        if (validationSpan) {
            validationSpan.style.display = 'none';
        }
    }
}

// Clear field validation styling
function clearFieldValidation(field) {
    field.classList.remove('input-validation-error');
    const validationSpan = field.parentElement.querySelector('.field-validation-error');
    if (validationSpan) {
        validationSpan.style.display = 'none';
    }
}

// Initialize delete confirmations
function initializeDeleteConfirmations() {
    const deleteLinks = document.querySelectorAll('a[href*="/Delete"]');

    deleteLinks.forEach(link => {
        link.addEventListener('click', function(e) {
            if (!confirm('Are you sure you want to delete this item?')) {
                e.preventDefault();
                return false;
            }
        });
    });
}

// Show loading spinner for form submissions
function showLoadingSpinner(button) {
    const originalText = button.textContent;
    button.textContent = 'Processing...';
    button.disabled = true;

    // Store original text to restore later
    button.setAttribute('data-original-text', originalText);

    // Add spinner
    const spinner = document.createElement('span');
    spinner.className = 'spinner-border spinner-border-sm me-2';
    spinner.setAttribute('role', 'status');
    spinner.setAttribute('aria-hidden', 'true');
    button.prepend(spinner);
}

// Hide loading spinner
function hideLoadingSpinner(button) {
    const originalText = button.getAttribute('data-original-text');
    const spinner = button.querySelector('.spinner-border');

    if (spinner) {
        spinner.remove();
    }

    if (originalText) {
        button.textContent = originalText;
        button.removeAttribute('data-original-text');
    }

    button.disabled = false;
}

// Auto-hide alerts after 5 seconds
document.addEventListener('DOMContentLoaded', function() {
    const alerts = document.querySelectorAll('.alert:not(.alert-permanent)');

    alerts.forEach(alert => {
        setTimeout(() => {
            if (alert && alert.parentNode) {
                alert.style.transition = 'opacity 0.5s ease-out';
                alert.style.opacity = '0';

                setTimeout(() => {
                    if (alert && alert.parentNode) {
                        alert.parentNode.removeChild(alert);
                    }
                }, 500);
            }
        }, 5000);
    });
});

// Utility functions
window.eShopModern = {
    // Format currency
    formatCurrency: function(amount, currency = 'USD') {
        return new Intl.NumberFormat('en-US', {
            style: 'currency',
            currency: currency
        }).format(amount);
    },

    // Format date
    formatDate: function(dateString) {
        const date = new Date(dateString);
        return date.toLocaleDateString('en-US', {
            year: 'numeric',
            month: 'short',
            day: 'numeric'
        });
    },

    // Debounce function for search
    debounce: function(func, wait, immediate) {
        var timeout;
        return function() {
            var context = this, args = arguments;
            var later = function() {
                timeout = null;
                if (!immediate) func.apply(context, args);
            };
            var callNow = immediate && !timeout;
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
            if (callNow) func.apply(context, args);
        };
    }
};