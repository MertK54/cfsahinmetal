<template>
  <div class="references-view py-4 py-md-5">
    <div class="container">
      <!-- Page Header -->
      <div class="text-center mb-4 mb-md-5">
        <span class="badge bg-warning mb-2">Referanslarımız</span>
        <h1 class="fw-bold theme-text display-5 mb-2">Yapılan İşler & Tamamlanan Projeler</h1>
        <p class="theme-text-muted lead max-w-700 mx-auto mb-0">
          Şahin Metal olarak müşterilerimiz için başarıyla imal ettiğimiz döküm arabaları, çelik yapılar, fason imalatlar ve saha montaj çalışmalarımız.
        </p>
      </div>

      <!-- Category Filter Pills (Swipeable on Mobile, Centered on Desktop) -->
      <div class="category-scroll-wrapper mb-4 mb-md-5">
        <div class="category-pills-bar d-flex gap-2">
          <button
            class="btn rounded-pill px-3.5 py-2 category-pill text-nowrap"
            :class="selectedCategory === null ? 'btn-warning' : 'btn-outline-warning'"
            @click="selectedCategory = null"
          >
            Tümü ({{ products.length }})
          </button>
          <button
            v-for="cat in categories"
            :key="cat.id"
            class="btn rounded-pill px-3.5 py-2 category-pill text-nowrap"
            :class="selectedCategory === cat.id ? 'btn-warning' : 'btn-outline-warning'"
            @click="selectedCategory = cat.id"
          >
            {{ cat.name }}
          </button>
        </div>
      </div>

      <!-- Loading State -->
      <div v-if="loading" class="text-center py-5">
        <div class="spinner-border text-warning" role="status">
          <span class="visually-hidden">Yükleniyor...</span>
        </div>
        <p class="theme-text-muted mt-2 fw-medium">Yapılan işler yükleniyor...</p>
      </div>

      <!-- Empty State -->
      <div v-else-if="filteredProducts.length === 0" class="text-center py-5 theme-card p-4 p-md-5">
        <i class="bi bi-folder-x fs-1 theme-text-muted d-block mb-3"></i>
        <h4 class="fw-bold theme-text">Henüz Bu Kategoride İş Bulunmuyor</h4>
        <p class="theme-text-muted mb-0">Seçilen kategori için henüz bir çalışma yüklenmemiş veya henüz eklenmemiş.</p>
      </div>

      <!-- References / Works Grid -->
      <div v-else class="row g-3 g-md-4">
        <div v-for="product in filteredProducts" :key="product.id" class="col-lg-4 col-md-6">
          <div class="card theme-card h-100 overflow-hidden work-card hover-lift">
            <!-- Work Image -->
            <div class="position-relative overflow-hidden cursor-pointer" @click="openModal(product)">
              <img
                :src="getImageUrl(product.imageUrl)"
                :alt="product.name"
                class="card-img-top work-image"
                @error="handleImageError"
                loading="lazy"
              />
              <div class="work-overlay d-flex align-items-center justify-content-center">
                <span class="btn btn-warning btn-sm rounded-circle p-2.5 p-sm-3 shadow">
                  <i class="bi bi-zoom-in fs-5"></i>
                </span>
              </div>
              <span class="glass-panel position-absolute top-0 start-0 m-2.5 m-sm-3 px-3 py-1 rounded-pill fw-semibold small theme-text">
                {{ getCategoryName(product.categoryId) }}
              </span>
            </div>

            <!-- Card Body -->
            <div class="card-body p-3.5 p-sm-4 d-flex flex-column">
              <h5 class="fw-bold theme-text mb-2 fs-6 fs-sm-5">{{ product.name }}</h5>
              <p class="theme-text-muted small line-clamp-3 mb-3 flex-grow-1" style="line-height: 1.6;">
                {{ product.description }}
              </p>

              <div class="pt-2.5 border-top d-flex align-items-center justify-content-between gap-2">
                <span class="theme-text-muted small fw-medium d-inline-flex align-items-center gap-1 text-nowrap">
                  <i class="bi bi-check-circle-fill text-success"></i> Tamamlanan İş
                </span>
                <button
                  @click="openModal(product)"
                  class="btn btn-sm btn-outline-warning rounded-pill px-3 py-1.5 fw-semibold d-inline-flex align-items-center gap-1 text-nowrap"
                >
                  <span>Detay Gör</span> <i class="bi bi-arrow-right"></i>
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- 100% RESPONSIVE & MOBILE-FRIENDLY DETAIL MODAL -->
    <div
      v-if="selectedProduct"
      class="modal fade show d-block modal-backdrop-soft"
      tabindex="-1"
      @click.self="closeModal"
    >
      <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg modal-dialog-touch">
        <div class="modal-content rounded-4 border-0 overflow-hidden theme-card shadow-lg">
          <!-- Modal Header -->
          <div class="modal-header theme-bg-alt border-bottom py-2.5 py-sm-3 px-3 px-sm-4 d-flex align-items-center justify-content-between">
            <h2 class="modal-title h6 fw-bold theme-text mb-0 d-flex align-items-center gap-2 text-truncate">
              <span class="icon-soft rounded-circle d-inline-flex align-items-center justify-content-center flex-shrink-0" style="width: 32px; height: 32px;">
                <i class="bi bi-award-fill small"></i>
              </span>
              <span class="text-truncate">{{ selectedProduct.name }}</span>
            </h2>
            <button
              type="button"
              class="btn-close-custom rounded-circle border-0 d-flex align-items-center justify-content-center"
              @click="closeModal"
              aria-label="Kapat"
            >
              <i class="bi bi-x-lg"></i>
            </button>
          </div>

          <!-- Modal Scrollable Body -->
          <div class="modal-body p-0">
            <div class="row g-0 flex-column flex-md-row">
              <!-- Modal Image Column -->
              <div class="col-md-6 theme-bg-alt d-flex align-items-center justify-content-center p-2.5 p-sm-3">
                <img
                  :src="getImageUrl(selectedProduct.imageUrl)"
                  :alt="selectedProduct.name"
                  class="img-fluid rounded-3 modal-product-img object-contain"
                />
              </div>

              <!-- Modal Info Column -->
              <div class="col-md-6 p-3 p-sm-4 d-flex flex-column justify-content-between">
                <div>
                  <span class="badge bg-warning mb-2 mb-sm-3 d-inline-block">
                    {{ getCategoryName(selectedProduct.categoryId) }}
                  </span>
                  <h3 class="h5 fw-bold theme-text mb-2">{{ selectedProduct.name }}</h3>
                  <p class="theme-text-muted small mb-3 leading-relaxed" style="line-height: 1.65;">
                    {{ selectedProduct.description }}
                  </p>

                  <!-- Technical specs if any -->
                  <div v-if="selectedProduct.materialType" class="p-2.5 rounded-3 theme-bg-alt mb-2 small theme-text">
                    <span class="theme-text-muted d-block small">Malzeme / Şase:</span>
                    <strong>{{ selectedProduct.materialType }}</strong>
                  </div>
                  <div v-if="selectedProduct.dimensions" class="p-2.5 rounded-3 theme-bg-alt mb-3 small theme-text">
                    <span class="theme-text-muted d-block small">Ölçüler / Kapasite:</span>
                    <strong>{{ selectedProduct.dimensions }}</strong>
                  </div>
                </div>

                <!-- Touch Action Buttons -->
                <div class="d-grid gap-2 pt-2 border-top mt-auto">
                  <a
                    :href="`https://wa.me/905326283789?text=${encodeURIComponent('Merhaba, web sitenizdeki ' + selectedProduct.name + ' işiniz hakkında benzer bir çalışma için fiyat teklifi almak istiyorum.')}`"
                    target="_blank"
                    class="btn btn-success btn-lg rounded-pill py-2.5 fw-bold d-flex align-items-center justify-content-center gap-2"
                  >
                    <i class="bi bi-whatsapp fs-5 pe-1"></i>
                    <span>Benzer İş İçin Teklif Al</span>
                  </a>
                  <button
                    type="button"
                    class="btn btn-outline-warning rounded-pill py-2 fw-semibold"
                    @click="closeModal"
                  >
                    Kapat
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted, watch } from 'vue'
import { useHead } from '@unhead/vue'
import { api } from '../../services/api'

useHead({
  title: 'Referanslarımız & Tamamlanan İşler | Şahin Metal',
  meta: [
    { name: 'description', content: 'Şahin Metal atölyesinde üretilen döküm taşıma arabaları, çelik çatı konstrüksiyonları, fason kaynak ve saha montaj referans projelerimiz.' },
    { property: 'og:title', content: 'Referanslarımız - Tamamlanan Projeler | Şahin Metal' }
  ]
})

const products = ref([])
const categories = ref([])
const loading = ref(true)
const selectedCategory = ref(null)
const selectedProduct = ref(null)

onMounted(async () => {
  try {
    const [prodData, catData] = await Promise.all([
      api.products.getAll(false),
      api.categories.getAll()
    ])
    products.value = prodData
    categories.value = catData
  } catch (err) {
    console.error('Veri çekme hatası:', err)
  } finally {
    loading.value = false
  }
})

const filteredProducts = computed(() => {
  if (!selectedCategory.value) return products.value
  return products.value.filter(p => p.categoryId === selectedCategory.value)
})

const getCategoryName = (catId) => {
  const cat = categories.value.find(c => c.id === catId)
  return cat ? cat.name : 'İmalat'
}

const getImageUrl = (url) => {
  if (!url) return '/images/services/hero_foundry.jpg'
  if (url.startsWith('http://') || url.startsWith('https://')) return url
  return 'http://localhost:5164' + url
}

const handleImageError = (e) => {
  e.target.src = 'http://localhost:5164/images/services/hero_foundry.jpg'
}

const openModal = (product) => {
  selectedProduct.value = product
  document.body.style.overflow = 'hidden'
}

const closeModal = () => {
  selectedProduct.value = null
  document.body.style.overflow = ''
}
</script>

<style scoped>
/* Swipeable Category Pill Bar */
.category-scroll-wrapper {
  overflow-x: auto;
  -webkit-overflow-scrolling: touch;
  scrollbar-width: none;
  padding-bottom: 0.25rem;
}
.category-scroll-wrapper::-webkit-scrollbar {
  display: none;
}

.category-pills-bar {
  width: max-content;
  margin: 0 auto;
}

@media (max-width: 767.98px) {
  .category-pills-bar {
    margin: 0;
    padding-left: 0.25rem;
    padding-right: 0.25rem;
  }
}

.category-pill {
  font-size: 0.9rem;
  font-weight: 600;
  min-height: 42px;
}

.cursor-pointer {
  cursor: pointer;
}

.work-image {
  height: 220px;
  object-fit: cover;
  transition: transform 0.45s cubic-bezier(0.16, 1, 0.3, 1);
}
.work-card:hover .work-image {
  transform: scale(1.05);
}

.work-overlay {
  position: absolute;
  inset: 0;
  background: rgba(15, 23, 42, 0.32);
  backdrop-filter: blur(2px);
  opacity: 0;
  transition: opacity 0.3s ease;
}
.work-card:hover .work-overlay {
  opacity: 1;
}

/* Modal Styling */
.modal-backdrop-soft {
  background-color: rgba(15, 23, 42, 0.65);
  backdrop-filter: blur(6px);
  z-index: 1055;
}

.modal-dialog-touch {
  margin: 0.75rem auto;
  max-width: 820px;
}

@media (max-width: 575.98px) {
  .modal-dialog-touch {
    margin: 0.5rem;
    max-height: calc(100vh - 1rem);
  }
}

.btn-close-custom {
  width: 36px;
  height: 36px;
  background: var(--bg-surface);
  color: var(--text-main);
  box-shadow: var(--shadow-soft);
  transition: all 0.2s ease;
}
.btn-close-custom:hover {
  background: var(--accent-primary);
  color: #fff;
}

.modal-product-img {
  max-height: 380px;
  width: 100%;
}

@media (max-width: 767.98px) {
  .modal-product-img {
    max-height: 220px;
  }
}

.line-clamp-3 {
  display: -webkit-box;
  -webkit-line-clamp: 3;
  -webkit-box-orient: vertical;
  overflow: hidden;
}

.max-w-700 {
  max-width: 68ch;
}
.object-contain {
  object-fit: contain;
}
</style>
