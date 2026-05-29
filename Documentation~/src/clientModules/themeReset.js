(function () {
  if (typeof window === 'undefined') return;
  var FLAG = 'achutils.theme-reset.v1';
  try {
    if (window.localStorage.getItem(FLAG) === '1') return;
    window.localStorage.removeItem('theme');
    window.localStorage.setItem(FLAG, '1');
    document.documentElement.setAttribute('data-theme', 'dark');
  } catch (e) {
    /* localStorage disabled */
  }
})();
