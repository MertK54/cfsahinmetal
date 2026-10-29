import axios from 'axios'

// API Base URL matches the http profile configured in backend launchSettings
const API_BASE_URL = 'http://localhost:5164/api'

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json'
  }
})

// Request Interceptor: Attach JWT Token from localStorage if present
apiClient.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('sm_token')
    if (token) {
      config.headers.Authorization = `Bearer ${token}`
    }
    return config
  },
  (error) => Promise.reject(error)
)

// Response Interceptor: Handle Unauthorized errors (419, 401)
apiClient.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response && error.response.status === 401) {
      // Clear token and redirect to login if session expired
      localStorage.removeItem('sm_token')
      localStorage.removeItem('sm_user')
      if (window.location.pathname.startsWith('/admin')) {
        window.location.href = '/login'
      }
    }
    return Promise.reject(error)
  }
)

export const api = {
  // Authentication
  auth: {
    login: async (username, password) => {
      const res = await apiClient.post('/auth/login', { username, password })
      if (res.data.token) {
        localStorage.setItem('sm_token', res.data.token)
        localStorage.setItem('sm_user', JSON.stringify({ username: res.data.username, role: res.data.role }))
      }
      return res.data
    },
    logout: () => {
      localStorage.removeItem('sm_token')
      localStorage.removeItem('sm_user')
    },
    getCurrentUser: () => {
      return JSON.parse(localStorage.getItem('sm_user') || 'null')
    },
    isAuthenticated: () => {
      return !!localStorage.getItem('sm_token')
    }
  },

  // Products CRUD
  products: {
    getAll: async (includeInactive = false) => {
      const res = await apiClient.get(`/products?includeInactive=${includeInactive}`)
      return res.data
    },
    getById: async (id) => {
      const res = await apiClient.get(`/products/${id}`)
      return res.data
    },
    getBySlug: async (slug) => {
      const res = await apiClient.get(`/products/slug/${slug}`)
      return res.data
    },
    create: async (productData) => {
      const res = await apiClient.post('/products', productData)
      return res.data
    },
    update: async (id, productData) => {
      const res = await apiClient.put(`/products/${id}`, productData)
      return res.data
    },
    delete: async (id) => {
      const res = await apiClient.delete(`/products/${id}`)
      return res.data
    }
  },

  // Categories
  categories: {
    getAll: async () => {
      const res = await apiClient.get('/categories')
      return res.data
    }
  },

  // Contact Submissions
  submissions: {
    getAll: async () => {
      const res = await apiClient.get('/contactsubmissions')
      return res.data
    },
    create: async (submissionData) => {
      const res = await apiClient.post('/contactsubmissions', submissionData)
      return res.data
    },
    markAsRead: async (id) => {
      const res = await apiClient.put(`/contactsubmissions/${id}/read`)
      return res.data
    }
  },

  // Site Settings
  settings: {
    get: async () => {
      const res = await apiClient.get('/sitesettings')
      return res.data
    },
    update: async (settingsData) => {
      const res = await apiClient.put('/sitesettings', settingsData)
      return res.data
    }
  },

  // File Upload
  upload: {
    uploadImage: async (file) => {
      const formData = new FormData()
      formData.append('file', file)
      const res = await apiClient.post('/upload', formData, {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      })
      return res.data // returns { Url: "/uploads/filename.jpg" }
    }
  }
}

export default api
