<template>
  <div class="contact-view py-4 py-md-5">
    <div class="container">
      <!-- Page Header -->
      <div class="text-center mb-4 mb-md-5">
        <span class="badge bg-warning mb-2">İletişim & Konum</span>
        <h1 class="fw-bold theme-text display-5 mb-2">Bize Ulaşın & Atölye Konumu</h1>
        <p class="theme-text-muted lead max-w-700 mx-auto mb-0">
          Çayırova / Kocaeli'deki atölyemize gelebilir veya hızlı iletişim numaralarımızdan hemen bize ulaşabilirsiniz.
        </p>
      </div>

      <div class="row g-4">
        <!-- Contact Info Card (Clean, Unified, No Clunky Nested Boxes) -->
        <div class="col-lg-5">
          <div class="card theme-card h-100 p-4 p-md-5 d-flex flex-column justify-content-between">
            <div>
              <h2 class="h5 fw-bold theme-text mb-4 d-flex align-items-center gap-2">
                <span class="icon-soft rounded-circle d-inline-flex align-items-center justify-content-center" style="width: 34px; height: 34px;">
                  <i class="bi bi-telephone-inbound-fill small"></i>
                </span>
                <span>İletişim Kanallarımız</span>
              </h2>

              <div class="contact-channels-list">
                <!-- Phone Channel -->
                <div class="contact-channel-item">
                  <div class="channel-icon-box channel-icon-blue rounded-circle d-flex align-items-center justify-content-center flex-shrink-0">
                    <i class="bi bi-telephone-fill"></i>
                  </div>
                  <div class="flex-grow-1 min-w-0">
                    <span class="theme-text-muted small d-block mb-1">Doğrudan Telefon</span>
                    <a href="tel:05326283789" class="text-warning text-decoration-none fw-bold fs-5 d-block">
                      0532 628 37 89
                    </a>
                  </div>
                </div>

                <!-- WhatsApp Channel -->
                <div class="contact-channel-item">
                  <div class="channel-icon-box channel-icon-green rounded-circle d-flex align-items-center justify-content-center flex-shrink-0">
                    <i class="bi bi-whatsapp pe-1"></i>
                  </div>
                  <div class="flex-grow-1 min-w-0">
                    <span class="theme-text-muted small d-block mb-1">WhatsApp Danışma Hattı</span>
                    <a
                      href="https://wa.me/905326283789"
                      target="_blank"
                      class="text-success text-decoration-none fw-bold fs-6 d-inline-flex align-items-center gap-1 mb-1"
                    >
                      <span>0532 628 37 89</span>
                      <i class="bi bi-arrow-up-right small"></i>
                    </a>
                    <small class="theme-text-muted d-block" style="font-size: 0.78rem;">Teknik çizim & hızlı fiyat teklifi</small>
                  </div>
                </div>

                <!-- Address Channel -->
                <div class="contact-channel-item">
                  <div class="channel-icon-box channel-icon-amber rounded-circle d-flex align-items-center justify-content-center flex-shrink-0">
                    <i class="bi bi-geo-alt-fill"></i>
                  </div>
                  <div class="flex-grow-1 min-w-0">
                    <span class="theme-text-muted small d-block mb-1">Atölye / Dükkan Adresi</span>
                    <p class="theme-text small fw-medium mb-1.5" style="line-height: 1.55;">
                      Çayırova Özgürlük Mahallesi, İstiklal Caddesi No: 64, Kocaeli
                    </p>
                    <a
                      href="https://maps.google.com/?q=Çayırova+Özgürlük+Mahallesi+İstiklal+Caddesi+No+64+Kocaeli"
                      target="_blank"
                      class="text-warning text-decoration-none small fw-semibold d-inline-flex align-items-center gap-1"
                    >
                      <span>Google Haritalarda Aç</span>
                      <i class="bi bi-box-arrow-up-right small"></i>
                    </a>
                  </div>
                </div>

                <!-- Email Channel -->
                <div class="contact-channel-item">
                  <div class="channel-icon-box channel-icon-slate rounded-circle d-flex align-items-center justify-content-center flex-shrink-0">
                    <i class="bi bi-envelope-fill"></i>
                  </div>
                  <div class="flex-grow-1 min-w-0">
                    <span class="theme-text-muted small d-block mb-1">E-Posta Adresi</span>
                    <a href="mailto:info@sahinmetal.com" class="theme-text text-decoration-none fw-semibold small">
                      info@sahinmetal.com
                    </a>
                  </div>
                </div>
              </div>
            </div>

            <!-- Big 1-Tap Call CTA Box at Bottom of Info Card -->
            <div class="p-3.5 rounded-4 theme-bg-alt mt-4 text-center">
              <span class="theme-text-muted small d-block mb-2">Hemen arayıp bilgi veya fiyat teklifi alabilirsiniz:</span>
              <a
                href="tel:05326283789"
                class="btn btn-warning w-100 rounded-pill py-2.5 fw-bold d-flex align-items-center justify-content-center gap-2 shadow-sm"
              >
                <i class="bi bi-telephone-fill pe-1"></i>
                <span>0532 628 37 89'u Ara</span>
              </a>
            </div>
          </div>
        </div>

        <!-- Contact Form Card -->
        <div class="col-lg-7">
          <div class="card theme-card h-100 p-4 p-md-5">
            <h2 class="h5 fw-bold theme-text mb-1 d-flex align-items-center gap-2">
              <span class="icon-soft rounded-circle d-inline-flex align-items-center justify-content-center" style="width: 34px; height: 34px;">
                <i class="bi bi-envelope-paper-fill small"></i>
              </span>
              <span>Bize Mesaj Gönderin</span>
            </h2>
            <p class="theme-text-muted small mb-4">Aşağıdaki formu doldurarak imalat ihtiyacınızı veya teklif talebinizi iletebilirsiniz.</p>

            <div v-if="successMsg" class="alert alert-success rounded-3 mb-3 d-flex align-items-center gap-2">
              <i class="bi bi-check-circle-fill fs-5 flex-shrink-0"></i>
              <span class="small fw-semibold">{{ successMsg }}</span>
            </div>
            <div v-if="errorMsg" class="alert alert-danger rounded-3 mb-3 d-flex align-items-center gap-2">
              <i class="bi bi-exclamation-triangle-fill fs-5 flex-shrink-0"></i>
              <span class="small fw-semibold">{{ errorMsg }}</span>
            </div>

            <form @submit.prevent="submitForm">
              <div class="row g-3">
                <!-- Ad Soyad -->
                <div class="col-md-6">
                  <label class="form-label fw-semibold theme-text small mb-1">Adınız Soyadınız *</label>
                  <input
                    v-model="form.name"
                    type="text"
                    class="form-control form-control-touch"
                    placeholder="Örn: Ahmet Yılmaz"
                    maxlength="80"
                    required
                  />
                </div>

                <!-- Cep Telefonu (Harf Yazılamaz - Otomatik Format) -->
                <div class="col-md-6">
                  <label class="form-label fw-semibold theme-text small mb-1 d-flex justify-content-between align-items-center">
                    <span>Cep Telefonu Numaranız *</span>
                    <span class="badge bg-success-subtle text-success border border-success-subtle py-0.5 px-2" style="font-size: 0.68rem;">
                      <i class="bi bi-shield-lock-fill me-1"></i>Sadece Rakam
                    </span>
                  </label>
                  <div class="input-group">
                    <span class="input-group-text bg-body-tertiary border-end-0">
                      <i class="bi bi-telephone-fill text-warning"></i>
                    </span>
                    <input
                      v-model="form.phone"
                      type="tel"
                      inputmode="numeric"
                      class="form-control form-control-touch border-start-0"
                      placeholder="05XX XXX XX XX"
                      maxlength="14"
                      required
                      @keydown="handlePhoneKeyDown"
                      @input="handlePhoneInput"
                      @paste="handlePhonePaste"
                    />
                  </div>
                  <small class="theme-text-muted d-block mt-1" style="font-size: 0.72rem;">Harf girişi engellenmiştir, numaranız otomatik formatlanır.</small>
                </div>

                <!-- E-Posta (İsteğe Bağlı) -->
                <div class="col-md-6">
                  <label class="form-label fw-semibold theme-text small mb-1">E-Posta Adresi (İsteğe Bağlı)</label>
                  <input
                    v-model="form.emailAddress"
                    type="email"
                    class="form-control form-control-touch"
                    placeholder="Örn: adiniz@ornek.com"
                    maxlength="100"
                  />
                </div>

                <!-- Konu / İmalat İhtiyacı -->
                <div class="col-md-6">
                  <label class="form-label fw-semibold theme-text small mb-1">Konu / İmalat İhtiyacı *</label>
                  <input
                    v-model="form.subject"
                    type="text"
                    class="form-control form-control-touch"
                    placeholder="Örn: Döküm Taşıma Arabası Teklifi"
                    maxlength="120"
                    required
                  />
                </div>

                <!-- Mesajınız -->
                <div class="col-12">
                  <label class="form-label fw-semibold theme-text small mb-1">Mesajınız / İş Detayları *</label>
                  <textarea
                    v-model="form.message"
                    class="form-control form-control-touch"
                    rows="4"
                    placeholder="Yapılacak iş hakkında adet, ölçü veya malzeme detaylarını iletebilirsiniz..."
                    maxlength="2000"
                    required
                  ></textarea>
                </div>

                <div class="col-12 pt-2">
                  <button type="submit" class="btn btn-warning btn-lg rounded-pill w-100 py-3 fw-bold d-flex align-items-center justify-content-center gap-2 shadow" :disabled="submitting">
                    <span v-if="submitting" class="spinner-border spinner-border-sm"></span>
                    <i v-else class="bi bi-send-fill fs-5 pe-1"></i>
                    <span>Mesajı Gönder</span>
                  </button>
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>

      <!-- Google Maps Shop Location Embed -->
      <div class="mt-4 mt-md-5">
        <h3 class="h5 fw-bold theme-text mb-3 text-center">Atölyemizin Harita Konumu</h3>
        <div class="theme-card p-2 overflow-hidden shadow-sm" style="height: 380px;">
          <iframe
            src="https://maps.google.com/maps?q=Çayırova+Özgürlük+Mahallesi+İstiklal+Caddesi+No+64+Kocaeli&t=&z=15&ie=UTF8&iwloc=&output=embed"
            width="100%"
            height="100%"
            style="border:0; border-radius: 0.85rem;"
            allowfullscreen=""
            loading="lazy"
            referrerpolicy="no-referrer-when-downgrade"
          ></iframe>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useHead } from '@unhead/vue'
import { api } from '../../services/api'
import { formatPhoneNumber, sanitizeInput, hasDangerousPayload } from '../../utils/security'

useHead({
  title: 'İletişim & Dükkan Konumu | Şahin Metal Çayırova Kocaeli',
  meta: [
    { name: 'description', content: 'Şahin Metal Özgürlük Mah. İstiklal Cd. No:64 Çayırova Kocaeli adresinde. Telefon: 0532 628 37 89. Teklif için bize ulaşın veya dükkanımızı ziyaret edin.' },
    { property: 'og:title', content: 'İletişim & Konum - Şahin Metal Çayırova' }
  ]
})

const form = ref({
  name: '',
  phone: '',
  emailAddress: '',
  subject: '',
  message: ''
})

const submitting = ref(false)
const successMsg = ref('')
const errorMsg = ref('')

// Strictly prevent typing letters into phone field
const handlePhoneKeyDown = (e) => {
  const allowedKeys = [
    'Backspace', 'Tab', 'Enter', 'Delete', 'ArrowLeft', 'ArrowRight', 'ArrowUp', 'ArrowDown', 'Home', 'End'
  ]
  if (allowedKeys.includes(e.key) || e.ctrlKey || e.metaKey) {
    return
  }

  // Reject non-numeric keys
  if (!/^[0-9]$/.test(e.key)) {
    e.preventDefault()
  }
}

// Ensure phone remains purely numeric and beautifully formatted
const handlePhoneInput = (e) => {
  form.value.phone = formatPhoneNumber(e.target.value)
}

// Clean pasted text from clipboard
const handlePhonePaste = (e) => {
  e.preventDefault()
  const pasted = (e.clipboardData || window.clipboardData)?.getData('text') || ''
  const digits = pasted.replace(/\D/g, '')
  form.value.phone = formatPhoneNumber(digits)
}

const submitForm = async () => {
  errorMsg.value = ''
  successMsg.value = ''

  // Validate phone: must have at least 10 digits
  const rawDigits = form.value.phone.replace(/\D/g, '')
  if (rawDigits.length < 10) {
    errorMsg.value = 'Lütfen geçerli bir cep telefonu numarası giriniz (en az 10 hane).'
    return
  }

  // Security check: Check for SQL injection or malicious script tags
  if (
    hasDangerousPayload(form.value.name) ||
    hasDangerousPayload(form.value.subject) ||
    hasDangerousPayload(form.value.message) ||
    hasDangerousPayload(form.value.emailAddress)
  ) {
    errorMsg.value = 'Güvenlik Uyarısı: Girdiğiniz metinde geçersiz veya zararlı karakter dizilimi tespit edildi.'
    return
  }

  submitting.value = true
  try {
    const contactInfo = form.value.emailAddress
      ? `${form.value.phone} | ${sanitizeInput(form.value.emailAddress, 100)}`
      : form.value.phone

    const payload = {
      name: sanitizeInput(form.value.name, 100),
      email: contactInfo,
      subject: sanitizeInput(form.value.subject, 150),
      message: sanitizeInput(form.value.message, 3000)
    }

    await api.submissions.create(payload)
    successMsg.value = 'Mesajınız başarıyla iletildi. En kısa sürede sizinle iletişime geçeceğiz.'
    form.value = {
      name: '',
      phone: '',
      emailAddress: '',
      subject: '',
      message: ''
    }
  } catch (err) {
    errorMsg.value = 'Mesaj gönderilirken bir hata oluştu. Lütfen telefon veya WhatsApp ile ulaşın.'
  } finally {
    submitting.value = false
  }
}
</script>

<style scoped>
.channel-icon-box {
  width: 44px;
  height: 44px;
  font-size: 1.15rem;
}

.channel-icon-blue {
  background-color: var(--accent-primary-soft);
  color: var(--accent-primary);
  border: 1px solid var(--accent-primary-border);
}

.channel-icon-green {
  background-color: rgba(16, 185, 129, 0.12);
  color: #10b981;
  border: 1px solid rgba(16, 185, 129, 0.25);
}

.channel-icon-amber {
  background-color: rgba(217, 119, 6, 0.12);
  color: #d97706;
  border: 1px solid rgba(217, 119, 6, 0.25);
}

.channel-icon-slate {
  background-color: var(--bg-surface-alt);
  color: var(--text-muted-custom);
  border: 1px solid var(--border-color-custom);
}

.contact-channels-list {
  display: flex;
  flex-direction: column;
}

.contact-channel-item {
  display: flex;
  align-items: flex-start;
  gap: 1rem;
  padding-top: 1.25rem;
  padding-bottom: 1.25rem;
  border-bottom: 1px solid var(--border-color-custom) !important;
}

.contact-channel-item:first-child {
  padding-top: 0.25rem;
}

.contact-channel-item:last-child {
  border-bottom: 0 !important;
  padding-bottom: 0.25rem;
}

.form-control-touch {
  font-size: 16px !important;
  min-height: 46px;
}

.max-w-700 {
  max-width: 68ch;
}
</style>
