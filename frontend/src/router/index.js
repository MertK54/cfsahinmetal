import { createRouter, createWebHistory } from 'vue-router'
import PublicLayout from '../views/public/PublicLayout.vue'
import HomeView from '../views/public/HomeView.vue'
import { api } from '../services/api'

const routes = [
  {
    path: '/',
    component: PublicLayout,
    children: [
      {
        path: '',
        name: 'Home',
        component: HomeView
      },
      {
        path: 'services',
        name: 'Services',
        component: () => import('../views/public/ServicesView.vue')
      },
      {
        path: 'references',
        name: 'References',
        component: () => import('../views/public/ProductsView.vue')
      },
      {
        path: 'products', // Alias for backward compatibility
        redirect: '/references'
      },
      {
        path: 'contact',
        name: 'Contact',
        component: () => import('../views/public/ContactView.vue')
      },
      {
        path: 'login',
        name: 'Login',
        component: () => import('../views/public/LoginView.vue')
      }
    ]
  },
  {
    path: '/admin',
    name: 'AdminDashboard',
    component: () => import('../views/admin/DashboardView.vue'),
    meta: { requiresAuth: true }
  },
  {
    path: '/:pathMatch(.*)*',
    redirect: '/'
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes,
  linkActiveClass: 'active-parent',
  linkExactActiveClass: 'active',
  scrollBehavior(to, from, savedPosition) {
    if (savedPosition) {
      return savedPosition
    }
    return { top: 0 }
  }
})

// Authentication Guard
router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (!api.auth.isAuthenticated()) {
      next('/login')
    } else {
      next()
    }
  } else {
    next()
  }
})

export default router
