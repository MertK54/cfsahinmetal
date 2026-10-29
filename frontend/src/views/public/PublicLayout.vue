<template>
  <div class="public-layout d-flex flex-column min-vh-100">
    <!-- Navigation Header -->
    <header class="sticky-top header-navbar">
      <div class="container">
        <nav class="navbar navbar-expand-lg py-2">
          <!-- Brand Logo -->
          <router-link to="/" class="navbar-brand d-flex align-items-center gap-2 text-decoration-none me-auto" @click="closeNav">
            <span class="brand-mark px-2 py-1 fs-6 rounded-3">ŞM</span>
            <div class="lh-sm">
              <span class="d-block theme-text fw-bold mb-0" style="font-size: 1.05rem; letter-spacing: -0.02em;">Şahin Metal</span>
              <small class="text-warning d-block fw-semibold" style="font-size: 0.72rem;">Metal Sanayi & İmalat</small>
            </div>
          </router-link>

          <!-- Mobile Theme Toggle & Menu Toggle Buttons -->
          <div class="d-flex align-items-center gap-2 d-lg-none">
            <button
              @click="toggleTheme"
              class="theme-toggle-btn"
              :title="currentTheme === 'dark' ? 'Açık Temaya Geç' : 'Koyu Temaya Geç'"
              aria-label="Tema Değiştir"
            >
              <i :class="currentTheme === 'dark' ? 'bi bi-sun-fill text-warning' : 'bi bi-moon-stars-fill'"></i>
            </button>

            <button
              class="navbar-toggler border-0 shadow-none p-2 rounded-3"
              type="button"
              data-bs-toggle="collapse"
              data-bs-target="#navbarMain"
              aria-controls="navbarMain"
              aria-expanded="false"
              aria-label="Menüyü Aç/Kapat"
              ref="navbarToggler"
            >
              <span class="navbar-toggler-icon"></span>
            </button>
          </div>

          <!-- Navbar Links Collapse -->
          <div class="collapse navbar-collapse" id="navbarMain" ref="navbarCollapse">
            <!-- Navigation Links -->
            <ul class="navbar-nav mx-auto mb-3 mb-lg-0 gap-1 gap-lg-1 pt-3 pt-lg-0">
              <li class="nav-item">
                <router-link to="/" class="nav-link" exact-active-class="active" @click="closeNav">
                  <i class="bi bi-house-door me-2 pe-1 d-lg-none"></i>Ana Sayfa
                </router-link>
              </li>
              <li class="nav-item">
                <router-link to="/services" class="nav-link" exact-active-class="active" @click="closeNav">
                  <i class="bi bi-gear-wide-connected me-2 pe-1 d-lg-none"></i>Hizmetlerimiz
                </router-link>
              </li>
              <li class="nav-item">
                <router-link to="/references" class="nav-link" exact-active-class="active" @click="closeNav">
                  <i class="bi bi-collection me-2 pe-1 d-lg-none"></i>Referanslarımız
                </router-link>
              </li>
              <li class="nav-item">
                <router-link to="/contact" class="nav-link" exact-active-class="active" @click="closeNav">
                  <i class="bi bi-geo-alt me-2 pe-1 d-lg-none"></i>İletişim & Konum
                </router-link>
              </li>
            </ul>

            <!-- Header Actions Group (Zero border on desktop!) -->
            <div class="header-actions-group d-flex flex-column flex-lg-row align-items-stretch align-items-lg-center gap-2 mt-3 mt-lg-0 pb-3 pb-lg-0 pt-3 pt-lg-0">
              <!-- Compact Desktop Theme Toggle Button (Perfect Circle) -->
              <button
                @click="toggleTheme"
                class="theme-toggle-btn d-none d-lg-inline-flex me-1"
                :title="currentTheme === 'dark' ? 'Açık Mod' : 'Koyu Mod'"
                aria-label="Tema Değiştir"
              >
                <i :class="currentTheme === 'dark' ? 'bi bi-sun-fill text-warning' : 'bi bi-moon-stars-fill'"></i>
              </button>

              <!-- Sleek WhatsApp Button -->
              <a
                href="https://wa.me/905326283789"
                target="_blank"
                class="btn btn-success btn-sm rounded-pill px-3 py-1.5 d-flex align-items-center justify-content-center gap-1.5 fw-semibold text-nowrap"
              >
                <i class="bi bi-whatsapp pe-1"></i>
                <span>WhatsApp</span>
              </a>

              <!-- Sleek Admin Panel Button -->
              <router-link
                to="/login"
                class="btn btn-outline-warning btn-sm rounded-pill px-3 py-1.5 d-flex align-items-center justify-content-center gap-1.5 fw-semibold text-nowrap"
                @click="closeNav"
              >
                <i class="bi bi-person-lock pe-1"></i>
                <span>Panel</span>
              </router-link>
            </div>
          </div>
        </nav>
      </div>
    </header>

    <!-- Main View Content -->
    <main class="flex-grow-1">
      <router-view />
    </main>

    <!-- Floating WhatsApp Button -->
    <a
      href="https://wa.me/905326283789?text=Merhaba,%20web%20sitenizden%20ulaşıyorum.%20Yapılan%20işler%20ve%20hizmetleriniz%20hakkında%20bilgi%20almak%20istiyorum."
      target="_blank"
      class="whatsapp-float shadow"
      title="WhatsApp ile İletişime Geçin"
      aria-label="WhatsApp İletişim"
    >
      <i class="bi bi-whatsapp pe-1"></i>
    </a>

    <!-- Footer -->
    <footer class="modern-footer pt-5 pb-4 mt-auto">
      <div class="container">
        <div class="row g-4 pb-2">
          <!-- Col 1: Brand & Phone Call CTA -->
          <div class="col-lg-5 col-md-6">
            <div class="d-flex align-items-center gap-2 mb-3">
              <span class="brand-mark px-2 py-1 fs-6 rounded-3">ŞM</span>
              <h5 class="fw-bold text-white mb-0">Şahin Metal</h5>
            </div>
            <p class="footer-muted small mb-3" style="max-width: 44ch; line-height: 1.7;">
              Döküm taşıma arabaları, taşıma sepetleri, tartı platformları, çelik çatı konstrüksiyon imalatı, fason kaynak, sulu kesim ve gemi kaynak montaj hizmetleri.
            </p>

            <!-- Big 1-Tap Call Button for Mobile Users -->
            <a
              href="tel:05326283789"
              class="footer-phone-btn text-decoration-none fw-bold d-inline-flex align-items-center justify-content-center gap-2 px-4 py-2.5 rounded-pill w-100 w-sm-auto mb-2"
            >
              <i class="bi bi-telephone-fill fs-5 pe-1"></i>
              <span>Hemen Ara: 0532 628 37 89</span>
            </a>
          </div>

          <!-- Col 2: Fast Links -->
          <div class="col-6 col-lg-3 col-md-6">
            <h6 class="fw-semibold text-white mb-3">Hızlı Bağlantılar</h6>
            <ul class="list-unstyled small mb-0 d-flex flex-column gap-2.5">
              <li><router-link to="/" class="footer-link text-decoration-none">Ana Sayfa</router-link></li>
              <li><router-link to="/services" class="footer-link text-decoration-none">Hizmetlerimiz</router-link></li>
              <li><router-link to="/references" class="footer-link text-decoration-none">Referanslarımız</router-link></li>
              <li><router-link to="/contact" class="footer-link text-decoration-none">İletişim & Konum</router-link></li>
            </ul>
          </div>

          <!-- Col 3: Contact Info -->
          <div class="col-12 col-lg-4 col-md-12">
            <h6 class="fw-semibold text-white mb-3">İletişim Bilgileri</h6>
            <ul class="list-unstyled footer-muted small mb-0 d-flex flex-column gap-2.5">
              <li class="d-flex align-items-start gap-2">
                <i class="bi bi-geo-alt-fill footer-accent mt-1 fs-6 flex-shrink-0"></i>
                <a
                  href="https://maps.google.com/?q=Çayırova+Özgürlük+Mahallesi+İstiklal+Caddesi+No+64+Kocaeli"
                  target="_blank"
                  class="footer-link text-decoration-none"
                >
                  Çayırova Özgürlük Mah. İstiklal Cad. No: 64, Kocaeli
                </a>
              </li>
              <li class="d-flex align-items-center gap-2">
                <i class="bi bi-telephone-fill footer-accent fs-6 flex-shrink-0"></i>
                <a href="tel:05326283789" class="footer-link text-decoration-none fw-semibold">0532 628 37 89</a>
              </li>
              <li class="d-flex align-items-center gap-2">
                <i class="bi bi-envelope-fill footer-accent fs-6 flex-shrink-0"></i>
                <a href="mailto:info@sahinmetal.com" class="footer-link text-decoration-none">info@sahinmetal.com</a>
              </li>
            </ul>
          </div>
        </div>

        <hr class="footer-divider my-4" />

        <div class="d-flex flex-column flex-md-row justify-content-between align-items-center footer-muted small gap-2 text-center text-md-start">
          <div>&copy; {{ new Date().getFullYear() }} Şahin Metal. Tüm Hakları Saklıdır.</div>
          <div><router-link to="/login" class="footer-link text-decoration-none">Yönetici Girişi</router-link></div>
        </div>
      </div>
    </footer>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useTheme } from '../../composables/useTheme'

const { currentTheme, toggleTheme } = useTheme()
const navbarCollapse = ref(null)

const closeNav = () => {
  if (navbarCollapse.value && navbarCollapse.value.classList.contains('show')) {
    const bsCollapse = window.bootstrap?.Collapse?.getInstance(navbarCollapse.value)
    if (bsCollapse) {
      bsCollapse.hide()
    } else {
      navbarCollapse.value.classList.remove('show')
    }
  }
}
</script>

<style scoped>
/* Perfect Circular Theme Button - Never stretched or swollen */
.theme-toggle-btn {
  width: 36px !important;
  height: 36px !important;
  min-width: 36px !important;
  min-height: 36px !important;
  max-width: 36px !important;
  max-height: 36px !important;
  padding: 0 !important;
  border-radius: 50% !important;
  display: inline-flex !important;
  align-items: center !important;
  justify-content: center !important;
  flex-shrink: 0 !important;
  background: var(--bg-surface-alt);
  color: var(--text-main);
  border: 1px solid var(--border-color-custom);
  cursor: pointer;
  transition: all 0.2s ease;
  line-height: 1;
}

.theme-toggle-btn:hover {
  background: var(--accent-primary-soft);
  color: var(--accent-primary);
  border-color: var(--accent-primary-border);
  transform: scale(1.05);
}

.theme-toggle-btn:active {
  transform: scale(0.95);
}

/* Zero border on desktop, mobile border-top only */
.header-actions-group {
  border: none !important;
}

@media (max-width: 991.98px) {
  .header-actions-group {
    border-top: 1px solid var(--border-color-custom) !important;
  }
}

.footer-muted {
  color: #94a3b8 !important;
}
.footer-accent {
  color: #60a5fa !important;
}
.footer-link {
  color: #94a3b8 !important;
  transition: color 0.2s ease;
}
.footer-link:hover {
  color: #f8fafc !important;
}
.footer-phone-btn {
  background: linear-gradient(135deg var(--in-oklab), #2563eb 0%, #1d4ed8 100%);
  color: #ffffff !important;
  box-shadow: 0 4px 14px rgba(37, 99, 235, 0.3);
  transition: all 0.2s ease;
}
.footer-phone-btn:hover {
  transform: translateY(-2px);
  box-shadow: 0 8px 20px rgba(37, 99, 235, 0.4);
}
.footer-divider {
  border-color: rgba(148, 163, 184, 0.16) !important;
}

@media (max-width: 991.98px) {
  .header-navbar .nav-link {
    border-radius: 0.75rem;
    padding: 0.75rem 1rem !important;
    font-size: 1.02rem;
  }
}
</style>
