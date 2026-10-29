<template>
  <div class="container py-5">
    <!-- Loading State -->
    <div v-if="loading" class="text-center py-5 my-5">
      <div class="spinner-border text-warning" role="status"></div>
      <p class="theme-text-muted small mt-2">Ürün detayları yükleniyor...</p>
    </div>

    <!-- Error State -->
    <div v-else-if="error" class="text-center py-5 my-5 theme-card p-5">
      <i class="bi bi-exclamation-triangle display-3 text-danger"></i>
      <h2 class="h4 fw-bold theme-text mt-3">Ürün Bulunamadı</h2>
      <p class="theme-text-muted small">Aradığınız ürün yayından kaldırılmış veya taşınmış olabilir.</p>
      <router-link to="/products" class="btn btn-warning rounded-pill px-4 mt-3">Kataloga Dön</router-link>
    </div>

    <!-- Product Details Content -->
    <div v-else class="fade-in mt-3">
      <!-- Breadcrumb -->
      <nav aria-label="breadcrumb" class="mb-4">
        <ol class="breadcrumb">
          <li class="breadcrumb-item"><router-link to="/" class="theme-text-muted text-decoration-none">Anasayfa</router-link></li>
          <li class="breadcrumb-item"><router-link to="/products" class="theme-text-muted text-decoration-none">Hizmetler & İmalat</router-link></li>
          <li class="breadcrumb-item active theme-text fw-medium" aria-current="page">{{ product.name }}</li>
        </ol>
      </nav>

      <div class="row g-4 g-lg-5">
        <!-- Product Image (Left Column) -->
        <div class="col-lg-6">
          <div class="card theme-card overflow-hidden p-2">
            <img
              :src="getImageUrl(product.imageUrl)"
              class="img-fluid product-detail-img rounded-3"
              :alt="product.name"
              @error="(e) => e.target.src = '/images/services/hero_foundry.jpg'"
            />
          </div>
        </div>

        <!-- Product Specs and Description (Right Column) -->
        <div class="col-lg-6 d-flex flex-column justify-content-between">
          <div>
            <span class="badge bg-warning mb-3">{{ categoryName }}</span>
            <h1 class="display-5 fw-bold theme-text mb-3">{{ product.name }}</h1>
            <p class="lead theme-text-muted fs-6 mb-4">{{ product.description }}</p>

            <!-- Technical attributes row -->
            <div class="row g-2 g-sm-3 mb-4">
              <div class="col-6 col-sm-4" v-if="product.materialType">
                <div class="p-3 theme-card text-center">
                  <span class="theme-text-muted small d-block mb-1">İmalat Tipi</span>
                  <span class="fw-bold theme-text small text-truncate d-block">{{ product.materialType }}</span>
                </div>
              </div>
              <div class="col-6 col-sm-4" v-if="product.weight">
                <div class="p-3 theme-card text-center">
                  <span class="theme-text-muted small d-block mb-1">Birim Ağırlık</span>
                  <span class="fw-bold theme-text small d-block">{{ product.weight }} kg</span>
                </div>
              </div>
              <div class="col-12 col-sm-4" v-if="product.dimensions">
                <div class="p-3 theme-card text-center">
                  <span class="theme-text-muted small d-block mb-1">Standart Ebat</span>
                  <span class="fw-bold theme-text small text-truncate d-block">{{ product.dimensions }}</span>
                </div>
              </div>
            </div>

            <!-- Specifications Table -->
            <div v-if="parsedSpecs && Object.keys(parsedSpecs).length" class="mb-4">
              <h2 class="h5 fw-bold theme-text mb-3 border-bottom pb-2">Teknik Özellikler</h2>
              <div class="table-responsive">
                <table class="table table-bordered small align-middle mb-0">
                  <tbody>
                    <tr v-for="(val, key) in parsedSpecs" :key="key">
                      <td class="fw-semibold theme-bg-alt theme-text text-nowrap" style="width: 35%;">{{ key }}</td>
                      <td class="theme-text">{{ val }}</td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>

          <!-- Actions -->
          <div class="mt-4 border-top pt-4">
            <router-link
              :to="{ path: '/contact', query: { product: product.name } }"
              class="btn btn-warning btn-lg rounded-pill w-100 w-sm-auto px-4 py-3 d-inline-flex align-items-center justify-content-center"
            >
              <i class="bi bi-file-earmark-text pe-1 me-2"></i>Bu Hizmet / İmalat İçin Özel Fiyat Teklifi Alın
            </router-link>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted, watch } from 'vue'
import { useRoute } from 'vue-router'
import { api } from '../../services/api'
import { useSeoMeta } from '@unhead/vue'

const route = useRoute()
const product = ref(null)
const categoryName = ref('Metal Alaşımı')
const loading = ref(true)
const error = ref(false)

const parsedSpecs = computed(() => {
  if (!product.value || !product.value.specifications) return null
  try {
    return JSON.parse(product.value.specifications)
  } catch (e) {
    console.error('Specs parsing error:', e)
    return null
  }
})

// Configure SEO dynamically when product data is loaded
const updateSeo = () => {
  if (product.value) {
    useSeoMeta({
      title: `${product.value.name} - Şahin Metal Teknik Katalog`,
      description: product.value.description,
      ogTitle: `${product.value.name} - Şahin Metal`,
      ogDescription: product.value.description,
      ogImage: product.value.imageUrl || 'https://sahinmetal.com/images/default-share.jpg'
    })
  }
}

const getImageUrl = (url) => {
  if (!url) return '/images/services/hero_foundry.jpg'
  if (url.startsWith('http://') || url.startsWith('https://')) return url
  if (url.startsWith('/uploads/')) return 'http://localhost:5164' + url
  if (url.startsWith('/images/')) return url
  return 'http://localhost:5164' + url
}

const fetchProduct = async () => {
  loading.value = true
  error.value = false
  try {
    const slug = route.params.slug
    product.value = await api.products.getBySlug(slug)
    categoryName.value = product.value.category?.name || 'Metal Alaşımı'
    updateSeo()
  } catch (err) {
    console.error('Ürün yüklenemedi:', err)
    error.value = true
  } finally {
    loading.value = false
  }
}

onMounted(fetchProduct)
watch(() => route.params.slug, fetchProduct)
</script>

<style scoped>
.product-detail-img {
  width: 100%;
  height: 450px;
  object-fit: cover;
}
@media (max-width: 768px) {
  .product-detail-img {
    height: 300px;
  }
}
@media (max-width: 576px) {
  .product-detail-img {
    height: 250px;
  }
}
.breadcrumb-item + .breadcrumb-item::before {
  color: var(--text-muted-custom);
}
.fade-in {
  animation: fadeIn 0.4s ease-out;
}
@keyframes fadeIn {
  from { opacity: 0; transform: translateY(10px); }
  to { opacity: 1; transform: translateY(0); }
}
</style>
