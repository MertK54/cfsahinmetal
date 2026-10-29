<template>
  <div class="home-view">
    <!-- Hero Slider / Carousel Section (Rock-solid Grid Stacking: Zero Layout Shift) -->
    <section
      class="hero-slider theme-hero-section position-relative border-bottom overflow-hidden"
      @mouseenter="pauseSlider"
      @mouseleave="startSlider"
      @touchstart="handleTouchStart"
      @touchend="handleTouchEnd"
    >
      <!-- Carousel Slides Stacked in CSS Grid: NO HEIGHT COLLAPSE OR JUMP -->
      <div class="slides-grid-wrapper">
        <div
          v-for="(slide, index) in slides"
          :key="index"
          class="slide-grid-item py-4 py-lg-5"
          :class="{ 'active-slide': activeSlide === index }"
        >
          <div class="container py-lg-3">
            <div class="row align-items-center g-4 g-lg-5">
              <!-- Slide Left Content -->
              <div class="col-lg-7 slide-text-col">
                <span class="badge bg-warning mb-2.5 mb-md-3 d-inline-block">
                  {{ slide.badge }}
                </span>

                <h1 class="display-4 display-lg-3 fw-bold theme-hero-title mb-2">
                  Şahin Metal
                </h1>
                <h2 class="h4 h3-md fw-semibold text-warning mb-3">
                  {{ slide.subtitle }}
                </h2>

                <p class="lead theme-hero-desc mb-3 mb-md-4 max-w-650">
                  {{ slide.description }}
                </p>

                <!-- Action Buttons -->
                <div class="d-flex flex-column flex-sm-row gap-2.5 gap-sm-3 pt-1">
                  <router-link :to="slide.buttonUrl" class="btn btn-warning btn-lg px-4 py-3 rounded-pill text-center">
                    <i :class="slide.buttonIcon" class="me-2 pe-1"></i> {{ slide.buttonText }}
                  </router-link>
                  <a
                    href="https://wa.me/905326283789"
                    target="_blank"
                    class="btn btn-success btn-lg px-4 py-3 rounded-pill text-center d-flex align-items-center justify-content-center gap-2"
                  >
                    <i class="bi bi-whatsapp fs-5 pe-1"></i>
                    <span>WhatsApp Danışma</span>
                  </a>
                </div>
              </div>

              <!-- Slide Right Image -->
              <div class="col-lg-5">
                <div class="position-relative slide-img-box mx-auto">
                  <div class="slide-img-frame rounded-4 overflow-hidden">
                    <img
                      :src="slide.image"
                      :alt="slide.subtitle"
                      class="img-fluid slide-img"
                    />
                  </div>
                  <!-- Floating Phone Badge -->
                  <a
                    href="tel:05326283789"
                    class="position-absolute bottom-0 end-0 m-2.5 m-sm-3 glass-panel px-3 py-2 rounded-pill fw-semibold small d-flex align-items-center gap-2 theme-text text-decoration-none shadow"
                  >
                    <span class="icon-soft rounded-circle d-inline-flex align-items-center justify-content-center" style="width: 30px; height: 30px;">
                      <i class="bi bi-telephone-fill" style="font-size: 0.8rem;"></i>
                    </span>
                    <span>0532 628 37 89</span>
                  </a>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Unified Soft Slider Bottom Navigation (Arrows + Indicators Together - NEVER overlaps content) -->
      <div class="slider-nav-bar d-flex align-items-center justify-content-center gap-3 pb-3 pt-1">
        <!-- Prev Arrow Button -->
        <button
          class="slider-nav-btn rounded-circle d-flex align-items-center justify-content-center"
          @click="prevSlide"
          title="Önceki Slayt"
          aria-label="Önceki Slayt"
        >
          <i class="bi bi-chevron-left"></i>
        </button>

        <!-- Slider Indicator Bars -->
        <div class="slider-indicators-pill px-3 py-1.5 rounded-pill d-flex align-items-center gap-2">
          <button
            v-for="(slide, index) in slides"
            :key="index"
            class="indicator-bar border-0 rounded-pill"
            :class="{ 'active-bar': activeSlide === index }"
            @click="goToSlide(index)"
            :title="`Slayt ${index + 1}`"
            :aria-label="`Slayt ${index + 1}`"
          ></button>
        </div>

        <!-- Next Arrow Button -->
        <button
          class="slider-nav-btn rounded-circle d-flex align-items-center justify-content-center"
          @click="nextSlide"
          title="Sonraki Slayt"
          aria-label="Sonraki Slayt"
        >
          <i class="bi bi-chevron-right"></i>
        </button>
      </div>
    </section>

    <!-- Statistics Counter Bar -->
    <section class="py-4 theme-counter-section position-relative">
      <div class="container py-1">
        <div class="row g-2.5 g-sm-3 g-md-4 text-center">
          <div class="col-6 col-md-3">
            <div class="p-3 p-md-4 rounded-4 theme-counter-card hover-lift h-100 d-flex flex-column justify-content-center">
              <div class="icon-soft rounded-3 mx-auto mb-2 mb-md-3 d-flex align-items-center justify-content-center" style="width: 44px; height: 44px;">
                <i class="bi bi-clock-history fs-5"></i>
              </div>
              <h2 class="fw-bold theme-text display-6 mb-1">15+ Yıl</h2>
              <p class="theme-counter-text small mb-0 fw-medium">Sektörel Tecrübe</p>
            </div>
          </div>
          <div class="col-6 col-md-3">
            <div class="p-3 p-md-4 rounded-4 theme-counter-card hover-lift h-100 d-flex flex-column justify-content-center">
              <div class="icon-soft rounded-3 mx-auto mb-2 mb-md-3 d-flex align-items-center justify-content-center" style="width: 44px; height: 44px;">
                <i class="bi bi-building-check fs-5"></i>
              </div>
              <h2 class="fw-bold theme-text display-6 mb-1">1.200+</h2>
              <p class="theme-counter-text small mb-0 fw-medium">Tamamlanan İmalat</p>
            </div>
          </div>
          <div class="col-6 col-md-3">
            <div class="p-3 p-md-4 rounded-4 theme-counter-card hover-lift h-100 d-flex flex-column justify-content-center">
              <div class="icon-soft rounded-3 mx-auto mb-2 mb-md-3 d-flex align-items-center justify-content-center" style="width: 44px; height: 44px;">
                <i class="bi bi-people-fill fs-5"></i>
              </div>
              <h2 class="fw-bold theme-text display-6 mb-1">450+</h2>
              <p class="theme-counter-text small mb-0 fw-medium">Mutlu Müşteri</p>
            </div>
          </div>
          <div class="col-6 col-md-3">
            <div class="p-3 p-md-4 rounded-4 theme-counter-card hover-lift h-100 d-flex flex-column justify-content-center">
              <div class="icon-soft rounded-3 mx-auto mb-2 mb-md-3 d-flex align-items-center justify-content-center" style="width: 44px; height: 44px;">
                <i class="bi bi-shield-check fs-5"></i>
              </div>
              <h2 class="fw-bold theme-text display-6 mb-1">%100</h2>
              <p class="theme-counter-text small mb-0 fw-medium">Zamanında Teslimat</p>
            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- 12 Sectors Summary Section -->
    <section class="py-4 py-md-5 theme-bg-surface">
      <div class="container py-lg-2">
        <div class="text-center mb-4 mb-md-5">
          <span class="badge bg-warning mb-2">Hizmet Verdiğimiz Endüstriler</span>
          <h2 class="fw-bold theme-text display-6 mb-2">12 Ana İmalat & Üretim Sektörü</h2>
          <p class="theme-text-muted lead max-w-700 mx-auto mb-0">
            Aşağıdaki endüstri alanlarının tümünde özel çelik aksam, döküm taşıma ekipmanları ve saha montaj imalatı yapmaktayız.
          </p>
        </div>

        <div class="row g-2.5 g-md-3">
          <div v-for="(sector, idx) in mainSectors" :key="idx" class="col-12 col-sm-6 col-lg-4">
            <router-link to="/services" class="text-decoration-none d-block h-100">
              <div class="p-3 theme-card d-flex align-items-center gap-3 sector-item hover-lift h-100">
                <span class="icon-soft rounded-3 d-inline-flex align-items-center justify-content-center flex-shrink-0" style="width: 36px; height: 36px;">
                  <i class="bi bi-check2-circle fs-5"></i>
                </span>
                <span class="fw-semibold theme-text flex-grow-1" style="font-size: 0.95rem;">{{ sector }}</span>
                <i class="bi bi-chevron-right text-muted small"></i>
              </div>
            </router-link>
          </div>
        </div>

        <div class="text-center mt-4 mt-md-5">
          <router-link to="/services" class="btn btn-warning rounded-pill px-4 py-3 w-100 w-sm-auto">
            Tüm Sektör Detaylarını İnceleyin <i class="bi bi-arrow-right ms-1"></i>
          </router-link>
        </div>
      </div>
    </section>

    <!-- 3 Step Manufacturing Process -->
    <section class="py-4 py-md-5 theme-bg-alt border-top border-bottom">
      <div class="container py-lg-2">
        <div class="text-center mb-4 mb-md-5">
          <span class="badge bg-warning mb-2">Çalışma Prensibimiz</span>
          <h2 class="fw-bold theme-text display-6 mb-2">3 Adımda Profesyonel İmalat</h2>
          <p class="theme-text-muted lead max-w-700 mx-auto mb-0">
            Projelerinizi zamanında, yüksek mukavemet garantisi ve standartlara uygun teslim ediyoruz.
          </p>
        </div>

        <div class="row g-3 g-md-4">
          <div class="col-md-4">
            <div class="card h-100 p-3.5 p-sm-4 text-center hover-lift theme-card">
              <div class="step-badge icon-soft fw-bold rounded-circle mx-auto mb-3">1</div>
              <h5 class="fw-bold theme-text mb-2 fs-6 fs-sm-5">Projelendirme & Keşif</h5>
              <p class="theme-text-muted small mb-0" style="line-height: 1.6;">
                Tesisinizde yerinde keşif yapılır, teknik çizimleriniz incelenir ve ihtiyacınıza uygun taşıma kapasitesi hesaplanır.
              </p>
            </div>
          </div>

          <div class="col-md-4">
            <div class="card h-100 p-3.5 p-sm-4 text-center hover-lift theme-card">
              <div class="step-badge icon-soft fw-bold rounded-circle mx-auto mb-3">2</div>
              <h5 class="fw-bold theme-text mb-2 fs-6 fs-sm-5">Hassas İmalat & Kaynak</h5>
              <p class="theme-text-muted small mb-0" style="line-height: 1.6;">
                Atölyemizde CNC sulu kesim, EN ISO sertifikalı argon/gazaltı kaynak ve ağır hizmet çelik şase montajı yapılır.
              </p>
            </div>
          </div>

          <div class="col-md-4">
            <div class="card h-100 p-3.5 p-sm-4 text-center hover-lift theme-card">
              <div class="step-badge icon-soft fw-bold rounded-circle mx-auto mb-3">3</div>
              <h5 class="fw-bold theme-text mb-2 fs-6 fs-sm-5">Kalite Kontrol & Teslimat</h5>
              <p class="theme-text-muted small mb-0" style="line-height: 1.6;">
                Ağır yük dayanım testleri tamamlanan ürünler tesisinize sevk edilir ve güvenli saha montajı gerçekleştirilir.
              </p>
            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- Quick Quote Callout Banner -->
    <section class="py-4 py-md-5 soft-quote-section">
      <div class="container">
        <div class="row align-items-center g-3 g-md-4">
          <div class="col-lg-8 text-center text-lg-start">
            <span class="badge bg-warning mb-2">Hızlı Teklif Hattı</span>
            <h3 class="fw-bold theme-text mb-2 display-6">Teknik Çizim veya Ölçülerinizi Gönderin</h3>
            <p class="lead theme-text-muted mb-0">
              Döküm arabası, sepet, çelik çatı veya kaynak ihtiyacınızı 24 saat içinde fiyatlandırıp teklif sunalım.
            </p>
          </div>
          <div class="col-lg-4 text-center text-lg-end">
            <a
              href="https://wa.me/905326283789?text=Merhaba,%20teknik%20çizim/ölçülerim%20var.%20Fiyat%20teklifi%20almak%20istiyorum."
              target="_blank"
              class="btn btn-warning btn-lg px-4 py-3 rounded-pill w-100 w-sm-auto d-inline-flex align-items-center justify-content-center gap-2"
            >
              <i class="bi bi-whatsapp fs-5 pe-1"></i>
              <span>Çizim / Ölçü Gönder</span>
            </a>
          </div>
        </div>
      </div>
    </section>

    <!-- Recent Works (Referanslar) Section -->
    <section class="py-4 py-md-5 theme-bg-surface">
      <div class="container py-lg-2">
        <div class="d-flex flex-column flex-sm-row align-items-sm-end justify-content-between mb-3 mb-md-4 gap-2">
          <div>
            <span class="badge bg-warning mb-2">Referanslarımız</span>
            <h2 class="fw-bold theme-text display-6 mb-0">Tamamlanan İşlerimiz</h2>
          </div>
          <router-link to="/references" class="btn btn-outline-warning rounded-pill px-3.5 py-2 align-self-start align-self-sm-auto text-nowrap">
            Tüm İşleri Gör ({{ products.length }}) <i class="bi bi-arrow-right ms-1"></i>
          </router-link>
        </div>

        <div v-if="products.length === 0" class="text-center py-5">
          <div class="spinner-border text-warning" role="status"></div>
        </div>

        <div v-else class="row g-3 g-md-4">
          <div v-for="product in products.slice(0, 3)" :key="product.id" class="col-12 col-md-6 col-lg-4">
            <div class="card h-100 overflow-hidden work-card hover-lift theme-card">
              <div class="overflow-hidden">
                <img :src="getImageUrl(product.imageUrl)" :alt="product.name" class="card-img-top work-img" loading="lazy" />
              </div>
              <div class="card-body p-3.5 p-sm-4 d-flex flex-column">
                <h5 class="fw-bold theme-text mb-2 fs-6 fs-sm-5">{{ product.name }}</h5>
                <p class="theme-text-muted small line-clamp-2 mb-3 flex-grow-1" style="line-height: 1.6;">{{ product.description }}</p>
                <div>
                  <router-link to="/references" class="btn btn-sm btn-outline-warning rounded-pill px-3 py-2 w-100 w-sm-auto text-center">
                    Detayları İncele <i class="bi bi-arrow-right ms-1"></i>
                  </router-link>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- Contact & Location CTA Section -->
    <section class="py-4 py-md-5 theme-hero-section border-top">
      <div class="container text-center py-2">
        <span class="badge bg-warning mb-2">Bize Ulaşın</span>
        <h2 class="fw-bold theme-hero-title mb-2 display-6">Atölyemizi Ziyaret Edin</h2>
        <p class="theme-hero-desc lead max-w-600 mx-auto mb-4">
          Çayırova Özgürlük Mahallesi, İstiklal Caddesi No: 64, Kocaeli adresindeki atölyemizde hizmetinizdeyiz.
        </p>
        <div class="d-flex flex-column flex-sm-row justify-content-center gap-2.5 gap-sm-3">
          <a href="tel:05326283789" class="btn btn-warning btn-lg px-4 py-3 rounded-pill d-inline-flex align-items-center justify-content-center gap-2">
            <i class="bi bi-telephone-fill fs-5 pe-1"></i>
            <span>0532 628 37 89'u Ara</span>
          </a>
          <router-link to="/contact" class="btn btn-outline-warning btn-lg px-4 py-3 rounded-pill d-inline-flex align-items-center justify-content-center gap-2">
            <i class="bi bi-geo-alt-fill fs-5 pe-1"></i>
            <span>Haritada Konumu Gör</span>
          </router-link>
        </div>
      </div>
    </section>
  </div>
</template>

<script setup>
import { ref, onMounted, onUnmounted } from 'vue'
import { useHead } from '@unhead/vue'
import { api } from '../../services/api'

useHead({
  title: 'Şahin Metal - Döküm Taşıma Arabaları & Çelik Çatı Konstrüksiyon İmalatı',
  meta: [
    { name: 'description', content: 'Döküm taşıma arabaları, sepetler, tartı platformları, fason kaynak, sulu kesim, çelik çatı ve gemi kaynak montaj hizmetleri. Çayırova / Kocaeli.' },
    { name: 'keywords', content: 'döküm taşıma arabası, çelik çatı, fason kaynak, sulu kesim, gemi kaynak montaj, çayırova kocaeli metal sanayi' },
    { property: 'og:title', content: 'Şahin Metal - Döküm Taşıma Arabaları & Metal Sanayi' },
    { property: 'og:description', content: 'Döküm imalatı, taşıma sepetleri, çelik konstrüksiyon ve fason kaynak hizmetleri.' },
    { property: 'og:image', content: '/images/services/hero_foundry.jpg' }
  ]
})

const products = ref([])

// Slider featured slides
const slides = [
  {
    badge: 'Öne Çıkan Hizmet #1',
    subtitle: 'Döküm Sanayi & Ağır Sanayi Ekipmanları',
    description: 'Dökümhaneler ve ağır sanayi tesisleri için yüksek taşıma kapasiteli döküm taşıma arabaları, cüruf arabaları, derece sepetleri ve ağır şase imalatı.',
    image: '/images/services/hero_foundry.jpg',
    buttonText: 'Yapılan İşler (Referanslar)',
    buttonUrl: '/references',
    buttonIcon: 'bi bi-images'
  },
  {
    badge: 'Öne Çıkan Hizmet #2',
    subtitle: 'Otomotiv & Makine İmalat Sanayi',
    description: 'Otomotiv montaj hatları ve makine imalatçılarına özel ağır hizmet taşıma kasaları, istif sepetleri, fason kaynak ve hassas sulu kesim üretimi.',
    image: '/uploads/service_3.jpg',
    buttonText: 'Hizmet Sektörlerimiz',
    buttonUrl: '/services',
    buttonIcon: 'bi bi-building-gear'
  },
  {
    badge: 'Öne Çıkan Hizmet #3',
    subtitle: 'Çelik Çatı & Gemi Kaynak Montajı',
    description: 'Fabrikalar ve endüstriyel tesisler için yüksek dayanımlı çelik çatı konstrüksiyon imalatı, tersane gemi kaynak ve saha montaj hizmetleri.',
    image: '/uploads/service_6.jpg',
    buttonText: 'Bize Ulaşın (İletişim)',
    buttonUrl: '/contact',
    buttonIcon: 'bi bi-geo-alt-fill'
  }
]

const activeSlide = ref(0)
let sliderTimer = null

const startSlider = () => {
  if (sliderTimer) clearInterval(sliderTimer)
  sliderTimer = setInterval(() => {
    nextSlide()
  }, 5000)
}

const pauseSlider = () => {
  if (sliderTimer) clearInterval(sliderTimer)
}

const nextSlide = () => {
  activeSlide.value = (activeSlide.value + 1) % slides.length
}

const prevSlide = () => {
  activeSlide.value = (activeSlide.value - 1 + slides.length) % slides.length
}

const goToSlide = (index) => {
  activeSlide.value = index
}

// Touch swipe support for mobile slider
let touchStartX = 0
let touchEndX = 0

const handleTouchStart = (e) => {
  touchStartX = e.changedTouches[0].screenX
  pauseSlider()
}

const handleTouchEnd = (e) => {
  touchEndX = e.changedTouches[0].screenX
  if (touchStartX - touchEndX > 50) {
    nextSlide()
  } else if (touchEndX - touchStartX > 50) {
    prevSlide()
  }
  startSlider()
}

const mainSectors = [
  'Otomotiv ve Yedek Parça İmalat Sanayi',
  'Ağır Sanayi Sektörü',
  'Makine İmalat Endüstrisi',
  'Beyaz Eşya ve Elektronik Sektörü',
  'Tekstil Boya, Dokuma, Konfeksiyon Sanayi',
  'Gıda, Tarım, Şarap Endüstrisi',
  'Döküm Sanayi',
  'Maden ve Taş Ocakları',
  'Çimento Endüstrisi',
  'Kimya ve İlaç Endüstrisi',
  'Kağıt ve Matbaa Endüstrisi',
  'Nakliye ve Lojistik Endüstrisi'
]

onMounted(async () => {
  startSlider()
  try {
    products.value = await api.products.getAll(false)
  } catch (err) {
    console.error('İşler çekilemedi:', err)
  }
})

onUnmounted(() => {
  pauseSlider()
})

const getImageUrl = (url) => {
  if (!url) return '/images/services/hero_foundry.jpg'
  if (url.startsWith('http://') || url.startsWith('https://')) return url
  return 'http://localhost:5164' + url
}
</script>

<style scoped>
.hero-slider {
  min-height: 480px;
}

/* CSS Grid Stacking: All slides occupy the same grid area so height is rock-solid! */
.slides-grid-wrapper {
  display: grid;
  grid-template-areas: "slide";
  align-items: center;
  position: relative;
  min-height: 440px;
}

.slide-grid-item {
  grid-area: slide;
  opacity: 0;
  visibility: hidden;
  transition: opacity 0.45s cubic-bezier(0.16, 1, 0.3, 1), transform 0.45s cubic-bezier(0.16, 1, 0.3, 1), visibility 0.45s ease;
  transform: translateY(6px);
  pointer-events: none;
  z-index: 1;
}

.slide-grid-item.active-slide {
  opacity: 1;
  visibility: visible;
  transform: translateY(0);
  pointer-events: auto;
  z-index: 2;
}

.slide-img-frame {
  border: 1px solid var(--border-color-custom);
  box-shadow: var(--shadow-md);
  background: var(--bg-surface);
  padding: 0.35rem;
}

.slide-img {
  max-height: 360px;
  width: 100%;
  object-fit: cover;
  border-radius: 0.85rem;
}

/* Modern Unified Bottom Navigation Bar */
.slider-nav-bar {
  z-index: 5;
}

.slider-nav-btn {
  width: 36px;
  height: 36px;
  background: var(--bg-surface);
  color: var(--text-main);
  border: 1px solid var(--border-color-custom);
  box-shadow: var(--shadow-soft);
  font-size: 0.9rem;
  cursor: pointer;
  transition: all 0.2s cubic-bezier(0.16, 1, 0.3, 1);
}

.slider-nav-btn:hover {
  background: var(--accent-primary);
  border-color: var(--accent-primary);
  color: #ffffff;
  transform: scale(1.06);
}

.slider-nav-btn:active {
  transform: scale(0.92);
}

.slider-indicators-pill {
  background: var(--bg-surface);
  border: 1px solid var(--border-color-custom);
  box-shadow: var(--shadow-soft);
}

.indicator-bar {
  width: 22px;
  height: 6px;
  background-color: rgba(148, 163, 184, 0.35);
  cursor: pointer;
  transition: all 0.3s ease;
}

.indicator-bar.active-bar {
  width: 40px;
  background-color: var(--accent-primary);
}

.step-badge {
  width: 48px;
  height: 48px;
  font-size: 1.25rem;
  display: flex;
  align-items: center;
  justify-content: center;
}

.sector-item {
  transition: all 0.25s ease;
}

.work-img {
  height: 200px;
  object-fit: cover;
  transition: transform 0.45s cubic-bezier(0.16, 1, 0.3, 1);
}

.work-card:hover .work-img {
  transform: scale(1.04);
}

.line-clamp-2 {
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden;
}

.max-w-700 { max-width: 68ch; }
.max-w-650 { max-width: 62ch; }
.max-w-600 { max-width: 58ch; }

@media (max-width: 767.98px) {
  .hero-slider {
    min-height: auto;
  }
  .slides-grid-wrapper {
    min-height: 420px;
  }
  .slide-img {
    max-height: 220px;
  }
  .slider-nav-btn {
    width: 34px;
    height: 34px;
  }
  .indicator-bar {
    width: 18px;
    height: 5px;
  }
  .indicator-bar.active-bar {
    width: 32px;
  }
}
</style>
