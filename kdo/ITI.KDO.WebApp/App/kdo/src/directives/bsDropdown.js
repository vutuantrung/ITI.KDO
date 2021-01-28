export default {
  mounted(el, binding) {
    const container = el.parentNode;

    el.addEventListener('click', e => {
      container.classList.toggle('open');
    });
  }
};
