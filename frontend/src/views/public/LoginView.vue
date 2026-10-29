<template>
  <div class="container py-5">
    <div class="row justify-content-center">
      <div class="col-md-5">
        <div class="card theme-card p-4 p-md-5">
          <div class="text-center mb-4">
            <div class="icon-soft mx-auto mb-3 d-flex align-items-center justify-content-center rounded-circle" style="width: 68px; height: 68px;">
              <i class="bi bi-shield-lock-fill fs-2"></i>
            </div>
            <h2 class="h4 fw-bold m-0 theme-text">Yönetici Girişi</h2>
            <p class="theme-text-muted small mt-2 mb-0">Şahin Metal İçerik Yönetim Sistemi</p>
          </div>

          <form @submit.prevent="handleLogin">
            <div class="mb-3">
              <label for="usernameInput" class="form-label small fw-semibold theme-text">Kullanıcı Adı</label>
              <input
                type="text"
                v-model="credentials.username"
                class="form-control"
                id="usernameInput"
                required
                placeholder="Örn: admin"
                autocomplete="username"
              />
            </div>

            <div class="mb-4">
              <label for="passwordInput" class="form-label small fw-semibold theme-text">Şifre</label>
              <input
                type="password"
                v-model="credentials.password"
                class="form-control"
                id="passwordInput"
                required
                placeholder="••••••••"
                autocomplete="current-password"
              />
            </div>

            <div v-if="errorMsg" class="alert alert-danger p-2 small mb-3 text-center rounded-3">
              {{ errorMsg }}
            </div>

            <button
              type="submit"
              class="btn btn-warning rounded-pill w-100 py-3 fw-semibold"
              :disabled="loading"
            >
              <span v-if="loading" class="spinner-border spinner-border-sm me-2"></span>
              Giriş Yap
            </button>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { api } from '../../services/api'
import { useSeoMeta } from '@unhead/vue'
import { sanitizeInput, hasDangerousPayload } from '../../utils/security'

useSeoMeta({
  title: 'Yönetici Girişi - Şahin Metal',
  description: 'Şahin Metal CMS yönetim paneli giriş ekranı.'
})

const router = useRouter()
const credentials = ref({ username: '', password: '' })
const errorMsg = ref('')
const loading = ref(false)

const handleLogin = async () => {
  errorMsg.value = ''
  
  const cleanUsername = sanitizeInput(credentials.value.username, 50)
  if (!cleanUsername || !credentials.value.password) {
    errorMsg.value = 'Lütfen kullanıcı adı ve şifrenizi giriniz.'
    return
  }

  // Guard against SQL injection attempts
  if (hasDangerousPayload(cleanUsername) || hasDangerousPayload(credentials.value.password)) {
    errorMsg.value = 'Güvenlik uyarısı: Geçersiz veya zararlı karakter dizilimi.'
    return
  }

  loading.value = true
  try {
    await api.auth.login(cleanUsername, credentials.value.password)
    router.push('/admin')
  } catch (err) {
    if (err.response && err.response.status === 401) {
      errorMsg.value = 'Hatalı kullanıcı adı veya şifre!'
    } else {
      errorMsg.value = 'Giriş yapılamadı. API sunucusunun çalıştığından ve MySQL bağlantısının aktif olduğundan emin olun.'
    }
  } finally {
    loading.value = false
  }
}
</script>
