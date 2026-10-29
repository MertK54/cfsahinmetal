<template>
  <div class="services-view py-4 py-md-5">
    <div class="container">
      <!-- Section Title -->
      <div class="text-center mb-4 mb-md-5">
        <span class="badge bg-warning mb-2">Hizmet Sektörlerimiz</span>
        <h1 class="fw-bold theme-text display-5 mb-2">Hizmet Verdiğimiz Endüstriler</h1>
        <p class="theme-text-muted lead max-w-700 mx-auto mb-0">
          Şahin Metal & Form Metal Sanayi olarak endüstriyel üretim, fason imalat, ağır döküm taşıma sistemleri ve kaynak montaj çözümlerimizle hizmet sunduğumuz başlıca sektörler:
        </p>
      </div>

      <!-- 12 Sectors Grid -->
      <div class="row g-3 g-md-4">
        <div v-for="(sector, index) in sectors" :key="index" class="col-12 col-md-6 col-lg-4">
          <div class="card theme-card h-100 overflow-hidden sector-card hover-lift">
            <div class="card-body p-3.5 p-sm-4">
              <div class="d-flex align-items-center gap-3 mb-2.5">
                <div class="icon-square icon-soft rounded-3 fs-4 flex-shrink-0">
                  <i :class="sector.icon"></i>
                </div>
                <div>
                  <span class="text-warning fw-semibold small d-block">Sektör #{{ index + 1 }}</span>
                  <h5 class="fw-bold theme-text mb-0 fs-6 fs-sm-5">{{ sector.title }}</h5>
                </div>
              </div>
              <p class="theme-text-muted small mb-0" style="line-height: 1.65;">
                {{ sector.description }}
              </p>
            </div>
            <div class="card-footer bg-transparent border-top px-3.5 px-sm-4 py-2.5 d-flex align-items-center justify-content-between">
              <small class="theme-text-muted fw-medium d-none d-sm-inline">Özel Metal Çözümleri</small>
              <router-link to="/references" class="btn btn-sm btn-outline-warning rounded-pill px-3 py-1.5 w-100 w-sm-auto text-center">
                İşlerimizi İnceleyin <i class="bi bi-arrow-right ms-1"></i>
              </router-link>
            </div>
          </div>
        </div>
      </div>

      <!-- Bottom Call To Action -->
      <div class="mt-4 mt-md-5 p-3.5 p-sm-4 p-md-5 soft-quote-section rounded-4 text-center position-relative overflow-hidden">
        <div class="position-relative z-1 py-1">
          <span class="badge bg-warning mb-2">Proje & İmalat Desteği</span>
          <h2 class="fw-bold theme-text mb-2 display-6">Özel Projeniz veya İmalat İhtiyacınız mı Var?</h2>
          <p class="theme-text-muted max-w-600 mx-auto mb-4 lead">
            Çayırova / Kocaeli atölyemizde döküm arabaları, çelik konstrüksiyon, fason kaynak ve talaşlı/sulu kesim imalatı ile hizmetinizdeyiz.
          </p>
          <div class="d-flex flex-column flex-sm-row justify-content-center gap-2.5 gap-sm-3">
            <a href="https://wa.me/905326283789" target="_blank" class="btn btn-success btn-lg px-4 py-3 rounded-pill d-inline-flex align-items-center justify-content-center gap-2">
              <i class="bi bi-whatsapp fs-5 pe-1"></i>
              <span>WhatsApp Teklif Hattı</span>
            </a>
            <router-link to="/contact" class="btn btn-outline-warning btn-lg px-4 py-3 rounded-pill d-inline-flex align-items-center justify-content-center gap-2">
              <i class="bi bi-geo-alt-fill fs-5 pe-1"></i>
              <span>İletişim & Konum</span>
            </router-link>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { useHead } from '@unhead/vue'

useHead({
  title: 'Hizmet Verdiğimiz Endüstriler - 12 Sektörde Çelik İmalat | Şahin Metal',
  meta: [
    { name: 'description', content: 'Otomotiv, döküm sanayi, ağır sanayi, çimento, tekstil, makine ve lojistik sektörlerine özel döküm arabaları, çelik aksam ve kaynaklı imalat.' },
    { property: 'og:title', content: 'Hizmet Verdiğimiz Endüstriler - Şahin Metal' }
  ]
})

const sectors = [
  {
    title: 'Otomotiv ve Yedek Parça İmalat Sanayi',
    icon: 'bi bi-car-front-fill',
    description: 'Otomotiv montaj hatları ve parça imalatçıları için ağır hizmet taşıma kasaları, montaj sepetleri ve fason çelik aksam üretimi.'
  },
  {
    title: 'Ağır Sanayi Sektörü',
    icon: 'bi bi-building-fill-gear',
    description: 'Demir-çelik tesisleri, dökümhaneler ve ağır sanayi fabrikaları için döküm taşıma arabaları, cüruf arabaları ve dayanımlı platform imalatı.'
  },
  {
    title: 'Makine İmalat Endüstrisi',
    icon: 'bi bi-cpu-fill',
    description: 'Endüstriyel makineler için şase imalatı, fason kaynaklı yapı grubu üretimi, işleme ve montaj desteği.'
  },
  {
    title: 'Beyaz Eşya ve Elektronik Sektörü',
    icon: 'bi bi-plugin',
    description: 'Seri üretim hatları ve depolama alanlarına özel hassas taşıma sepetleri, istifleme rafları ve özel çelik aparatlar.'
  },
  {
    title: 'Tekstil Boya, Dokuma, Konfeksiyon Sanayi',
    icon: 'bi bi-grid-3x3-gap-fill',
    description: 'Boya ve terbiye hatları için paslanmaz taşıma arabaları, bobin sepetleri, kumaş taşıma arabaları ve saha konstrüksiyonu.'
  },
  {
    title: 'Gıda, Tarım, Şarap Endüstrisi',
    icon: 'bi bi-flower1',
    description: 'Hijyenik standartlarda paslanmaz çelik (AISI 304/316) yıkama tezgahları, karıştırma tankları ve taşıma ekipmanları.'
  },
  {
    title: 'Döküm Sanayi',
    icon: 'bi bi-fire',
    description: 'Yüksek sıcaklığa dayanıklı döküm taşıma potaları, derece arabaları, döküm sepetleri ve ağır yük platform imalatı.'
  },
  {
    title: 'Maden ve Taş Ocakları',
    icon: 'bi bi-tools',
    description: 'Ağır çalışma şartlarına uygun aşınmaya dayanıklı konveyör şaseleri, elek kasaları ve fason sert dolgu kaynak işleri.'
  },
  {
    title: 'Çimento Endüstrisi',
    icon: 'bi bi-bricks',
    description: 'Çimento fabrikaları ve hazır beton tesisleri için ağır yapı çelik montajı, borulama ve söküm-takım hizmetleri.'
  },
  {
    title: 'Kimya ve İlaç Endüstrisi',
    icon: 'bi bi-eyedropper',
    description: 'Asit ve kimyasallara dayanıklı paslanmaz depolama rafları, ızgara platformlar ve sızdırmaz yıkama teçhizatları.'
  },
  {
    title: 'Kağıt ve Matbaa Endüstrisi',
    icon: 'bi bi-journal-text',
    description: 'Ağır bobin ve kağıt rulolarının emniyetli taşınması için özel rulolu arabalar, konveyör ayakları ve yapı elemanları.'
  },
  {
    title: 'Nakliye ve Lojistik Endüstrisi',
    icon: 'bi bi-truck',
    description: 'Depo ve lojistik merkezleri için katlanabilir tel sepetler, ağır hizmet taşıma paletleri ve rampa konstrüksiyon imalatı.'
  }
]
</script>

<style scoped>
.icon-square {
  width: 48px;
  height: 48px;
  display: flex;
  align-items: center;
  justify-content: center;
}
.max-w-700 {
  max-width: 68ch;
}
.max-w-600 {
  max-width: 58ch;
}
</style>
