// USS Security — language toggle, smooth-scroll, scroll-reveal, counter animations
window.ussToggleLang = function () {
    try {
        var urlParams = new URLSearchParams(window.location.search);
        var current = urlParams.get('lang') || 'en';
        var newLang = current === 'en' ? 'bn' : 'en';
        try { localStorage.setItem('uss-lang', newLang); } catch (e) { }
        var newUrl = window.location.pathname + '?lang=' + newLang + window.location.hash;
        window.location.href = newUrl;
    } catch (e) {
        console.error('USS language toggle failed:', e);
    }
};

window.addEventListener('load', function () {
    var urlParams = new URLSearchParams(window.location.search);
    var lang = urlParams.get('lang');
    if (!lang) {
        try { lang = localStorage.getItem('uss-lang') || 'en'; } catch (e) { lang = 'en'; }
    }
    document.documentElement.lang = lang;
    var ls = document.getElementById('loading-screen');
    if (ls) { ls.style.pointerEvents = 'none'; setTimeout(function () { ls.style.display = 'none'; }, 500); }
});

document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('a[href^="#"]').forEach(function (a) {
        a.addEventListener('click', function (e) {
            var href = a.getAttribute('href');
            if (href && href.length > 1) {
                var target = document.querySelector(href);
                if (target) { e.preventDefault(); target.scrollIntoView({ behavior: 'smooth', block: 'start' }); }
            }
        });
    });

    if ('IntersectionObserver' in window) {
        var observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) { entry.target.classList.add('revealed'); observer.unobserve(entry.target); }
            });
        }, { threshold: 0.12, rootMargin: '0px 0px -60px 0px' });
        document.querySelectorAll('[data-reveal], [data-reveal-left]').forEach(function (el) { observer.observe(el); });
    }

    var counters = document.querySelectorAll('[data-counter]');
    if (counters.length && 'IntersectionObserver' in window) {
        var cObserver = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    var el = entry.target;
                    var target = parseInt(el.dataset.counter || '0', 10);
                    var suffix = el.dataset.suffix || '';
                    var prefix = el.dataset.prefix || '';
                    var start = 0; var duration = 2200; var startTime = null;
                    function step(ts) {
                        if (!startTime) startTime = ts;
                        var progress = Math.min((ts - startTime) / duration, 1);
                        var eased = 1 - Math.pow(1 - progress, 3);
                        el.textContent = prefix + Math.floor(eased * target) + suffix;
                        if (progress < 1) requestAnimationFrame(step);
                        else el.textContent = prefix + target + suffix;
                    }
                    requestAnimationFrame(step);
                    cObserver.unobserve(el);
                }
            });
        }, { threshold: 0.5 });
        counters.forEach(function (c) { cObserver.observe(c); });
    }
});
