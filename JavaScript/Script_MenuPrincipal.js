/* Cuando hago CLICK .stl-menu-button, .stl-menu-nav TOGGLE 'activo' */
const button = document.querySelector('.stl-menu-button')
const nav = document.querySelector('.stl-menu-nav')

button.addEventListener('click', () => {
    nav.classList.toggle('activo')
})