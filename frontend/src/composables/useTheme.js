import { ref, onMounted } from 'vue'

const currentTheme = ref('light')

export function useTheme() {
  const initTheme = () => {
    const savedTheme = localStorage.getItem('sm_theme')
    if (savedTheme === 'dark' || savedTheme === 'light') {
      currentTheme.value = savedTheme
    } else {
      currentTheme.value = 'light' // Default to crisp clean light mode
    }
    applyTheme(currentTheme.value)
  }

  const toggleTheme = () => {
    currentTheme.value = currentTheme.value === 'dark' ? 'light' : 'dark'
    localStorage.setItem('sm_theme', currentTheme.value)
    applyTheme(currentTheme.value)
  }

  const applyTheme = (theme) => {
    document.documentElement.setAttribute('data-bs-theme', theme)
    document.body.setAttribute('data-bs-theme', theme)
    if (theme === 'dark') {
      document.body.classList.add('dark-mode')
      document.body.classList.remove('light-mode')
    } else {
      document.body.classList.add('light-mode')
      document.body.classList.remove('dark-mode')
    }
  }

  onMounted(() => {
    initTheme()
  })

  return {
    currentTheme,
    toggleTheme,
    initTheme
  }
}
