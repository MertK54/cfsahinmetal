<template>
  <div class="admin-dashboard min-vh-100 pb-5">
    <!-- Admin Header -->
    <header class="header-navbar py-2 py-md-3 sticky-top">
      <div class="container-fluid px-3 px-md-4">
        <div class="d-flex align-items-center justify-content-between gap-2">
          <!-- Logo & Brand Title -->
          <div class="d-flex align-items-center gap-2 min-w-0">
            <span class="brand-mark px-2 py-1 fs-6 flex-shrink-0">ŞM</span>
            <div class="lh-sm min-w-0">
              <h1 class="h6 fw-bold theme-text mb-0 text-truncate" style="font-size: 1rem;">
                Şahin Metal
              </h1>
              <span class="text-warning fw-semibold d-block" style="font-size: 0.72rem;">Yönetici Paneli</span>
            </div>
          </div>

          <!-- Top Actions (Siteye Git & Çıkış) -->
          <div class="d-flex align-items-center gap-2 flex-shrink-0">
            <router-link
              to="/"
              target="_blank"
              class="btn btn-outline-warning btn-sm rounded-pill px-3 py-1.5 d-flex align-items-center gap-1.5 text-nowrap"
            >
              <i class="bi bi-box-arrow-up-right pe-1"></i>
              <span class="d-none d-sm-inline">Siteyi Gör</span>
              <span class="d-inline d-sm-none">Site</span>
            </router-link>
            <button
              @click="logout"
              class="btn btn-danger btn-sm rounded-pill px-3 py-1.5 fw-semibold d-flex align-items-center gap-1.5 text-nowrap">
              <i class="bi bi-box-arrow-right pe-1"></i>
              <span>Çıkış</span>
            </button>
          </div>
        </div>
      </div>
    </header>

    <!-- Main Admin Container -->
    <div class="container-fluid px-2.5 px-sm-3 px-md-4 py-3 py-md-4">
      <!-- MOBILE / TABLET TAB BAR (100% Responsive, Touch Friendly) -->
      <div class="d-lg-none mb-3">
        <div class="mobile-tabs-grid p-1.5 rounded-4 theme-card shadow-sm">
          <button
            class="mobile-tab-btn"
            :class="{ active: activeTab === 'add' }"
            @click="activeTab = 'add'"
          >
            <i class="bi bi-plus-circle-fill"></i>
            <span>{{ editingProduct ? 'Düzenle' : 'İş Ekle' }}</span>
          </button>

          <button
            class="mobile-tab-btn"
            :class="{ active: activeTab === 'list' }"
            @click="activeTab = 'list'"
          >
            <i class="bi bi-collection-fill"></i>
            <span>İşlerim <span class="tab-badge">({{ products.length }})</span></span>
          </button>

          <button
            class="mobile-tab-btn"
            :class="{ active: activeTab === 'messages' }"
            @click="activeTab = 'messages'"
          >
            <i class="bi bi-envelope-paper-fill"></i>
            <span>Mesajlar <span class="tab-badge">({{ submissions.length }})</span></span>
          </button>
        </div>
      </div>

      <div class="row g-4">
        <!-- DESKTOP SIDEBAR TABS (Only visible on lg+ screens) -->
        <div class="col-lg-3 d-none d-lg-block">
          <div class="card theme-card overflow-hidden sticky-top" style="top: 86px;">
            <div class="card-body p-3">
              <div class="nav flex-column nav-pills gap-2" role="tablist">
                <button
                  class="nav-link text-start py-3 px-3 fw-semibold rounded-3 d-flex align-items-center gap-3 admin-tab-btn"
                  :class="{ active: activeTab === 'add' }"
                  @click="activeTab = 'add'"
                >
                  <i class="bi bi-plus-circle-fill fs-5"></i>
                  <div>
                    <div class="lh-1">{{ editingProduct ? 'İşi Düzenle' : 'Yeni İş / Referans Ekle' }}</div>
                    <small class="fw-normal opacity-75" style="font-size: 0.75rem;">Fotoğraf ve açıklama yükleyin</small>
                  </div>
                </button>

                <button
                  class="nav-link text-start py-3 px-3 fw-semibold rounded-3 d-flex align-items-center gap-3 admin-tab-btn"
                  :class="{ active: activeTab === 'list' }"
                  @click="activeTab = 'list'"
                >
                  <i class="bi bi-collection-fill fs-5"></i>
                  <div>
                    <div class="lh-1">Yüklü Referanslarımız</div>
                    <small class="fw-normal opacity-75" style="font-size: 0.75rem;">({{ products.length }} adet çalışma)</small>
                  </div>
                </button>

                <button
                  class="nav-link text-start py-3 px-3 fw-semibold rounded-3 d-flex align-items-center gap-3 admin-tab-btn"
                  :class="{ active: activeTab === 'messages' }"
                  @click="activeTab = 'messages'"
                >
                  <i class="bi bi-envelope-paper-fill fs-5"></i>
                  <div>
                    <div class="lh-1">Gelen Mesajlar</div>
                    <small class="fw-normal opacity-75" style="font-size: 0.75rem;">({{ submissions.length }} adet talep)</small>
                  </div>
                </button>
              </div>
            </div>
          </div>
        </div>

        <!-- Tab Content Area -->
        <div class="col-lg-9">
          <!-- Notification Alerts -->
          <div v-if="alertMsg" class="alert alert-success alert-dismissible fade show rounded-3 shadow-sm mb-3 d-flex align-items-center" role="alert">
            <i class="bi bi-check-circle-fill fs-5 me-2 flex-shrink-0"></i>
            <div class="small fw-semibold flex-grow-1">{{ alertMsg }}</div>
            <button type="button" class="btn-close" @click="alertMsg = ''"></button>
          </div>
          <div v-if="errorMsg" class="alert alert-danger alert-dismissible fade show rounded-3 shadow-sm mb-3 d-flex align-items-center" role="alert">
            <i class="bi bi-exclamation-triangle-fill fs-5 me-2 flex-shrink-0"></i>
            <div class="small fw-semibold flex-grow-1">{{ errorMsg }}</div>
            <button type="button" class="btn-close" @click="errorMsg = ''"></button>
          </div>

          <!-- TAB 1: ADD NEW REFERENCE / WORK -->
          <div v-if="activeTab === 'add'" class="card theme-card p-3 p-sm-4 p-md-5">
            <div class="d-flex flex-column flex-sm-row sm-align-items-center justify-content-between pb-3.5 mb-3.5 border-bottom gap-2">
              <div>
                <h2 class="h5 fw-bold theme-text mb-1 d-flex align-items-center gap-2">
                  <i class="bi bi-cloud-arrow-up-fill text-warning"></i>
                  <span>{{ editingProduct ? 'Referans İşini Düzenle' : 'Yeni İş / Referans Ekle' }}</span>
                </h2>
                <p class="theme-text-muted small mb-0">Müşterilerinize göstermek istediğiniz tamamlanan işi buraya yükleyin.</p>
              </div>
              <button
                v-if="editingProduct"
                @click="cancelEdit"
                class="btn btn-outline-warning btn-sm rounded-pill px-3 py-1.5 align-self-start align-self-sm-auto text-nowrap"
              >
                <i class="bi bi-x-circle pe-1"></i> Vazgeç / İptal Et
              </button>
            </div>

            <form @submit.prevent="saveProduct">
              <div class="row g-3 g-md-4">
                <!-- Category Select -->
                <div class="col-md-6">
                  <label class="form-label fw-semibold theme-text d-flex align-items-center justify-content-between">
                    <span>1. İşin Kategorisi *</span>
                    <small class="theme-text-muted fw-normal">Zorunlu</small>
                  </label>
                  <select v-model="form.categoryId" class="form-select form-control-touch" required>
                    <option :value="null" disabled>-- Kategori Seçin --</option>
                    <option v-for="cat in categories" :key="cat.id" :value="cat.id">
                      {{ cat.name }}
                    </option>
                  </select>
                </div>

                <!-- Title -->
                <div class="col-md-6">
                  <label class="form-label fw-semibold theme-text d-flex align-items-center justify-content-between">
                    <span>2. Yapılan İşin Başlığı / Adı *</span>
                    <small class="theme-text-muted fw-normal">Zorunlu</small>
                  </label>
                  <input
                    v-model="form.name"
                    type="text"
                    class="form-control form-control-touch"
                    placeholder="Örn: Ağır Döküm Taşıma Arabası 10 Ton"
                    required
                  />
                </div>

                <!-- Description -->
                <div class="col-12">
                  <label class="form-label fw-semibold theme-text d-flex align-items-center justify-content-between">
                    <span>3. İş Açıklaması & Detayları *</span>
                    <small class="theme-text-muted fw-normal">Zorunlu</small>
                  </label>
                  <textarea
                    v-model="form.description"
                    class="form-control form-control-touch"
                    rows="4"
                    placeholder="İşin detayları, nerede kullanıldığı veya özellikleri hakkında bilgi verin..."
                    required
                  ></textarea>
                </div>

                <!-- Image Upload Box (Big Touch Friendly for Mobile) -->
                <div class="col-12">
                  <label class="form-label fw-semibold theme-text d-flex align-items-center justify-content-between">
                    <span>4. İşin Fotoğrafı *</span>
                    <small class="theme-text-muted fw-normal">Zorunlu</small>
                  </label>

                  <div class="upload-dropzone p-3 p-md-4 text-center rounded-4 position-relative">
                    <div v-if="uploading" class="py-4">
                      <div class="spinner-border text-warning" role="status"></div>
                      <p class="theme-text-muted small mt-2 mb-0 fw-semibold">Fotoğraf yükleniyor, lütfen bekleyin...</p>
                    </div>

                    <!-- Uploaded Image Preview -->
                    <div v-else-if="form.imageUrl" class="py-2">
                      <div class="preview-img-container mb-3 mx-auto">
                        <img :src="getImageUrl(form.imageUrl)" class="img-fluid rounded-3 preview-img" alt="Yüklenen Görsel" />
                      </div>
                      <div class="d-flex flex-wrap justify-content-center gap-2">
                        <span class="badge bg-success rounded-pill px-3 py-2 d-inline-flex align-items-center gap-1">
                          <i class="bi bi-check2-circle fs-6 pe-1"></i> Görsel Hazır
                        </span>
                        <label class="btn btn-outline-warning btn-sm rounded-pill px-3 py-2 cursor-pointer d-inline-flex align-items-center gap-1">
                          <i class="bi bi-arrow-repeat pe-1"></i> Başka Fotoğraf Seç
                          <input type="file" @change="handleFileUpload" accept="image/*" class="d-none" />
                        </label>
                      </div>
                    </div>

                    <!-- Empty Upload Prompt -->
                    <div v-else class="py-3 py-md-4">
                      <div class="icon-soft rounded-circle mx-auto mb-2 mb-md-3 d-flex align-items-center justify-content-center" style="width: 58px; height: 58px;">
                        <i class="bi bi-camera-fill fs-3"></i>
                      </div>
                      <h6 class="fw-bold theme-text mb-1">Fotoğraf Yüklemek İçin Dokunun</h6>
                      <p class="theme-text-muted small mb-3">Kameradan çekebilir veya galeriden resim seçebilirsiniz</p>
                      <label class="btn btn-warning px-4 py-2.5 rounded-pill cursor-pointer fw-semibold shadow-sm d-inline-flex align-items-center gap-2">
                        <i class="bi bi-image fs-5 pe-1"></i>
                        <span>Fotoğraf Seç / Çek</span>
                        <input type="file" @change="handleFileUpload" accept="image/*" class="d-none" />
                      </label>
                    </div>
                  </div>
                </div>

                <!-- Optional Details -->
                <div class="col-md-6">
                  <label class="form-label theme-text-muted small fw-semibold">Malzeme / Şase Tipi (İsteğe Bağlı)</label>
                  <input
                    v-model="form.materialType"
                    type="text"
                    class="form-control form-control-touch"
                    placeholder="Örn: Ağır Çelik NPU / NPI Konstrüksiyon"
                  />
                </div>
                <div class="col-md-6">
                  <label class="form-label theme-text-muted small fw-semibold">Ölçüler / Kapasite (İsteğe Bağlı)</label>
                  <input
                    v-model="form.dimensions"
                    type="text"
                    class="form-control form-control-touch"
                    placeholder="Örn: 2000x1500 mm / 10 Ton Kapasiteli"
                  />
                </div>

                <!-- Submit Button -->
                <div class="col-12 pt-2">
                  <button
                    type="submit"
                    class="btn btn-warning btn-lg rounded-pill w-100 py-3 fw-bold fs-6 d-flex align-items-center justify-content-center gap-2 shadow"
                    :disabled="saving || uploading"
                  >
                    <span v-if="saving" class="spinner-border spinner-border-sm"></span>
                    <i v-else class="bi bi-check-circle-fill fs-5 pe-1"></i>
                    <span>{{ editingProduct ? 'Güncellemeleri Kaydet' : 'Sitede Yayınla' }}</span>
                  </button>
                </div>
              </div>
            </form>
          </div>

          <!-- TAB 2: LIST OF REFERENCES (100% Mobile Optimized, Zero Cramping!) -->
          <div v-if="activeTab === 'list'">
            <!-- Mobile Header Bar -->
            <div class="d-flex align-items-center justify-content-between pb-3.5 mb-3.5 border-bottom px-1">
              <div>
                <h2 class="h5 fw-bold theme-text mb-0">Yüklü Referanslar ({{ products.length }})</h2>
                <small class="theme-text-muted d-none d-sm-block">Yayındaki ve kayıtlı tüm işleriniz</small>
              </div>
              <button @click="activeTab = 'add'" class="btn btn-warning btn-sm rounded-pill px-3 py-1.5 fw-semibold d-inline-flex align-items-center gap-1 shadow-sm">
                <i class="bi bi-plus-lg pe-1"></i> <span>Yeni Ekle</span>
              </button>
            </div>

            <!-- Empty State -->
            <div v-if="products.length === 0" class="card theme-card p-5 text-center">
              <i class="bi bi-inbox fs-1 theme-text-muted d-block mb-2"></i>
              <p class="theme-text-muted mb-0">Henüz hiç iş eklenmemiş.</p>
            </div>

            <div v-else>
              <!-- MOBILE CARDS (< 768px screens): Spacious, clean, no cutoffs! -->
              <div class="d-md-none d-flex flex-column gap-3">
                <div
                  v-for="prod in products"
                  :key="prod.id"
                  class="admin-work-card theme-card p-3 rounded-4 shadow-sm"
                >
                  <!-- Card Header: Image + Full Info -->
                  <div class="d-flex gap-3 align-items-start">
                    <!-- Thumbnail -->
                    <div class="flex-shrink-0">
                      <img
                        :src="getImageUrl(prod.imageUrl)"
                        class="rounded-3 object-fit-cover shadow-sm admin-thumb"
                        alt="İş Görseli"
                      />
                    </div>

                    <!-- Details (Uncramped, Full Width) -->
                    <div class="flex-grow-1 min-w-0">
                      <!-- Badges Row: Category on left, Status on right -->
                      <div class="d-flex align-items-center justify-content-between gap-1 mb-1.5">
                        <span class="badge bg-warning text-truncate" style="max-width: 140px; font-size: 0.7rem;">
                          {{ getCategoryName(prod.categoryId) }}
                        </span>
                        <span
                          :class="prod.isActive ? 'badge bg-success' : 'badge bg-danger'"
                          style="font-size: 0.7rem;"
                        >
                          {{ prod.isActive ? 'Yayında' : 'Pasif' }}
                        </span>
                      </div>

                      <!-- Title: Wraps naturally up to 2 lines without cutting off letters! -->
                      <h3 class="fw-bold theme-text mb-1 admin-item-title">
                        {{ prod.name }}
                      </h3>

                      <!-- Short Description -->
                      <p class="theme-text-muted small mb-0 admin-item-desc">
                        {{ prod.description }}
                      </p>
                    </div>
                  </div>

                  <!-- Action Buttons: Clean, balanced, touch-friendly -->
                  <div class="d-flex gap-2 pt-3 mt-2.5 border-top">
                    <button
                      @click="editProduct(prod)"
                      class="btn-action-edit flex-grow-1 py-2 rounded-pill fw-semibold d-flex align-items-center justify-content-center gap-1.5"
                    >
                      <i class="bi bi-pencil-fill pe-1"></i>
                      <span>Düzenle</span>
                    </button>
                    <button
                      @click="deleteProduct(prod.id)"
                      class="btn-action-delete flex-grow-1 py-2 rounded-pill fw-semibold d-flex align-items-center justify-content-center gap-1.5"
                    >
                      <i class="bi bi-trash-fill pe-1"></i>
                      <span>Sil</span>
                    </button>
                  </div>
                </div>
              </div>

              <!-- DESKTOP TABLE (>= 768px screens) -->
              <div class="d-none d-md-block card theme-card p-4">
                <div class="table-responsive">
                  <table class="table table-hover align-middle mb-0">
                    <thead class="theme-bg-alt">
                      <tr>
                        <th style="width: 80px;">Görsel</th>
                        <th>İş Adı / Başlık</th>
                        <th>Kategori</th>
                        <th>Durum</th>
                        <th style="width: 170px;" class="text-end">İşlemler</th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr v-for="prod in products" :key="prod.id">
                        <td>
                          <img :src="getImageUrl(prod.imageUrl)" class="rounded-3 object-fit-cover shadow-sm" style="width: 60px; height: 60px;" />
                        </td>
                        <td>
                          <div class="fw-bold theme-text">{{ prod.name }}</div>
                          <small class="theme-text-muted d-block text-truncate max-w-300">{{ prod.description }}</small>
                        </td>
                        <td>
                          <span class="badge bg-warning">{{ getCategoryName(prod.categoryId) }}</span>
                        </td>
                        <td>
                          <span :class="prod.isActive ? 'badge bg-success rounded-pill px-3 py-1' : 'badge bg-danger rounded-pill px-3 py-1'">
                            {{ prod.isActive ? 'Yayında' : 'Pasif' }}
                          </span>
                        </td>
                        <td class="text-end">
                          <button @click="editProduct(prod)" class="btn btn-sm btn-outline-warning rounded-pill px-2.5 py-1 me-1.5" title="Düzenle">
                            <i class="bi bi-pencil-fill pe-1"></i> Düzenle
                          </button>
                          <button @click="deleteProduct(prod.id)" class="btn btn-sm btn-outline-danger rounded-pill px-2.5 py-1" title="Sil">
                            <i class="bi bi-trash-fill pe-1"></i> Sil
                          </button>
                        </td>
                      </tr>
                    </tbody>
                  </table>
                </div>
              </div>
            </div>
          </div>

          <!-- TAB 3: CONTACT MESSAGES (Gelen Müşteri Talepleri) -->
          <div v-if="activeTab === 'messages'" class="card theme-card p-3 p-sm-4 p-md-5">
            <div class="d-flex flex-column flex-sm-row justify-content-between align-items-start align-items-sm-center pb-3.5 mb-3.5 border-bottom gap-2">
              <div>
                <h2 class="h5 fw-bold theme-text mb-1 d-flex align-items-center gap-2">
                  <i class="bi bi-inbox-fill text-warning"></i>
                  <span>Gelen İletişim & Teklif Mesajları ({{ submissions.length }})</span>
                </h2>
                <p class="theme-text-muted small mb-0">Müşterilerin sitenizdeki <strong>"İletişim & Konum"</strong> formundan gönderdiği talepler.</p>
              </div>
              <router-link
                to="/contact"
                target="_blank"
                class="btn btn-outline-warning btn-sm rounded-pill px-3 py-1.5 d-inline-flex align-items-center gap-1.5 text-nowrap"
              >
                <i class="bi bi-box-arrow-up-right pe-1"></i>
                <span>İletişim Formunu Gör</span>
              </router-link>
            </div>

            <!-- Empty State: Clear explanation and test link -->
            <div v-if="submissions.length === 0" class="text-center py-5 px-3">
              <div class="icon-soft rounded-circle mx-auto mb-3 d-flex align-items-center justify-content-center" style="width: 64px; height: 64px;">
                <i class="bi bi-envelope-open-fill fs-3 text-warning"></i>
              </div>
              <h6 class="fw-bold theme-text mb-2 fs-5">Henüz Gelen Mesaj Yok</h6>
              <p class="theme-text-muted small mb-4 mx-auto" style="max-width: 480px; line-height: 1.65;">
                Bu alan bir mesaj gönderme formu değil, müşterilerinizin web sitenizdeki <strong>İletişim & Konum</strong> sayfasından gönderdiği fiyat teklifi ve imalat taleplerinin düştüğü <strong>yönetici gelen kutusudur</strong>.
              </p>
              <div class="d-flex flex-wrap justify-content-center gap-2">
                <router-link
                  to="/contact"
                  target="_blank"
                  class="btn btn-warning rounded-pill px-4 py-2.5 fw-semibold d-inline-flex align-items-center gap-2 shadow-sm"
                >
                  <i class="bi bi-send-fill pe-1"></i>
                  <span>Formu Aç & Test Mesajı Gönder</span>
                </router-link>
              </div>
            </div>

            <!-- Message List -->
            <div v-else class="d-flex flex-column gap-3">
              <div
                v-for="sub in submissions"
                :key="sub.id"
                class="card theme-card p-3.5 p-sm-4 rounded-4 border"
                :class="{ 'border-primary shadow-sm': !sub.isRead }"
              >
                <div class="d-flex flex-column flex-sm-row justify-content-between align-items-start gap-2 mb-2">
                  <div class="d-flex align-items-center gap-2">
                    <span v-if="!sub.isRead" class="badge bg-warning rounded-pill px-2 py-1" style="font-size: 0.68rem;">Yeni</span>
                    <div>
                      <h6 class="fw-bold theme-text mb-0 fs-6">{{ sub.name }}</h6>
                      <span class="text-warning fw-semibold small">{{ sub.email }}</span>
                    </div>
                  </div>
                  <div class="d-flex align-items-center gap-2">
                    <span class="badge theme-bg-alt theme-text-muted fw-normal" style="font-size: 0.75rem;">
                      {{ formatDate(sub.submittedAt) }}
                    </span>
                    <button
                      v-if="!sub.isRead"
                      @click="markSubmissionAsRead(sub)"
                      class="btn btn-outline-secondary btn-sm rounded-pill px-2.5 py-0.5 text-nowrap"
                      style="font-size: 0.72rem;"
                      title="Okundu olarak işaretle"
                    >
                      <i class="bi bi-check2 pe-1"></i> Okundu
                    </button>
                  </div>
                </div>

                <div class="fw-semibold theme-text mb-2 small">
                  <span class="theme-text-muted">Konu / İhtiyaç:</span> {{ sub.subject }}
                </div>

                <p class="theme-text theme-bg-alt p-3 rounded-3 mb-3 small" style="line-height: 1.6;">
                  {{ sub.message }}
                </p>

                <!-- Quick Action Buttons to call or reply on mobile/desktop -->
                <div class="d-flex flex-wrap gap-2 pt-2.5 mt-2.5 border-top">
                  <a
                    :href="isPhoneNumber(sub.email) ? `tel:${sub.email}` : `mailto:${sub.email}`"
                    class="btn btn-outline-warning btn-sm rounded-pill px-3 py-1.5 d-inline-flex align-items-center gap-1.5"
                  >
                    <i :class="isPhoneNumber(sub.email) ? 'bi bi-telephone-fill pe-1' : 'bi bi-envelope-fill pe-1'"></i>
                    <span>{{ isPhoneNumber(sub.email) ? 'Müşteriyi Ara' : 'E-Posta Gönder' }}</span>
                  </a>
                  <a
                    v-if="isPhoneNumber(sub.email)"
                    :href="`https://wa.me/${cleanPhone(sub.email)}`"
                    target="_blank"
                    class="btn btn-success btn-sm rounded-pill px-3 py-1.5 d-inline-flex align-items-center gap-1.5"
                  >
                    <i class="bi bi-whatsapp pe-1"></i>
                    <span>WhatsApp'tan Yanıtla</span>
                  </a>
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
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { api } from '../../services/api'
import { sanitizeInput, hasDangerousPayload } from '../../utils/security'

const router = useRouter()
const activeTab = ref('add')

const products = ref([])
const categories = ref([])
const submissions = ref([])

const alertMsg = ref('')
const errorMsg = ref('')

const uploading = ref(false)
const saving = ref(false)
const editingProduct = ref(null)

const form = ref({
  categoryId: 1,
  name: '',
  description: '',
  imageUrl: '',
  materialType: '',
  dimensions: '',
  isActive: true
})

onMounted(async () => {
  await loadData()
})

const loadData = async () => {
  try {
    const [pData, cData, sData] = await Promise.all([
      api.products.getAll(true),
      api.categories.getAll(),
      api.submissions.getAll()
    ])
    products.value = pData
    categories.value = cData
    submissions.value = sData
    if (categories.value.length > 0 && !form.value.categoryId) {
      form.value.categoryId = categories.value[0].id
    }
  } catch (err) {
    errorMsg.value = 'Veriler yüklenirken bir hata oluştu.'
  }
}

const handleFileUpload = async (e) => {
  const file = e.target.files[0]
  if (!file) return

  uploading.value = true
  errorMsg.value = ''
  try {
    const res = await api.upload.uploadImage(file)
    form.value.imageUrl = res.url || res.Url
    alertMsg.value = 'Fotoğraf başarıyla yüklendi.'
  } catch (err) {
    errorMsg.value = 'Fotoğraf yüklenemedi. Maksimum boyut 5MB olmalıdır.'
  } finally {
    uploading.value = false
  }
}

const saveProduct = async () => {
  if (!form.value.imageUrl) {
    errorMsg.value = 'Lütfen yapılan işin bir fotoğrafını yükleyin.'
    return
  }

  // Security check: Guard against script and SQL injection vectors
  if (
    hasDangerousPayload(form.value.name) ||
    hasDangerousPayload(form.value.description) ||
    hasDangerousPayload(form.value.materialType) ||
    hasDangerousPayload(form.value.dimensions)
  ) {
    errorMsg.value = 'Güvenlik Uyarısı: Form alanlarında geçersiz veya zararlı karakter dizilimi tespit edildi.'
    return
  }

  const payload = {
    ...form.value,
    name: sanitizeInput(form.value.name, 200),
    description: sanitizeInput(form.value.description, 4000),
    materialType: sanitizeInput(form.value.materialType, 150),
    dimensions: sanitizeInput(form.value.dimensions, 100)
  }

  saving.value = true
  alertMsg.value = ''
  errorMsg.value = ''

  try {
    if (editingProduct.value) {
      await api.products.update(editingProduct.value.id, payload)
      alertMsg.value = 'Çalışma başarıyla güncellendi.'
    } else {
      await api.products.create(payload)
      alertMsg.value = 'Yeni iş başarıyla yüklendi ve sitede yayınlandı.'
    }

    resetForm()
    await loadData()
    activeTab.value = 'list'
  } catch (err) {
    errorMsg.value = 'İş kaydedilirken bir hata oluştu.'
  } finally {
    saving.value = false
  }
}

const editProduct = (prod) => {
  editingProduct.value = prod
  form.value = {
    categoryId: prod.categoryId,
    name: prod.name,
    description: prod.description,
    imageUrl: prod.imageUrl,
    materialType: prod.materialType || '',
    dimensions: prod.dimensions || '',
    isActive: prod.isActive
  }
  activeTab.value = 'add'
  window.scrollTo({ top: 0, behavior: 'smooth' })
}

const cancelEdit = () => {
  editingProduct.value = null
  resetForm()
}

const deleteProduct = async (id) => {
  if (!confirm('Bu çalışmayı silmek istediğinizden emin misiniz?')) return

  try {
    await api.products.delete(id)
    alertMsg.value = 'Çalışma silindi.'
    await loadData()
  } catch (err) {
    errorMsg.value = 'Silme işleminde hata oluştu.'
  }
}

const resetForm = () => {
  editingProduct.value = null
  form.value = {
    categoryId: categories.value.length > 0 ? categories.value[0].id : 1,
    name: '',
    description: '',
    imageUrl: '',
    materialType: '',
    dimensions: '',
    isActive: true
  }
}

const getCategoryName = (catId) => {
  const c = categories.value.find(cat => cat.id === catId)
  return c ? c.name : 'Genel'
}

const getImageUrl = (url) => {
  if (!url) return '/images/services/hero_foundry.jpg'
  if (url.startsWith('http://') || url.startsWith('https://')) return url
  return 'http://localhost:5164' + url
}

const formatDate = (dateStr) => {
  if (!dateStr) return ''
  return new Date(dateStr).toLocaleString('tr-TR', {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric',
    hour: '2-digit',
    minute: '2-digit'
  })
}

const isPhoneNumber = (val) => {
  if (!val) return false
  const digits = val.replace(/\D/g, '')
  return digits.length >= 10 && !val.includes('@')
}

const cleanPhone = (val) => {
  if (!val) return ''
  let cleaned = val.replace(/\D/g, '')
  if (cleaned.startsWith('0')) cleaned = '9' + cleaned
  if (!cleaned.startsWith('90') && cleaned.length === 10) cleaned = '90' + cleaned
  return cleaned
}

const markSubmissionAsRead = async (sub) => {
  try {
    await api.submissions.markAsRead(sub.id)
    sub.isRead = true
  } catch (err) {
    console.error('Failed to mark as read', err)
  }
}

const logout = () => {
  api.auth.logout()
  router.push('/login')
}
</script>

<style scoped>
/* Mobile Tab Grid: 3 Equal Buttons, Zero Cutoffs */
.mobile-tabs-grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 0.35rem;
  background-color: var(--card-bg);
}

.mobile-tab-btn {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  gap: 0.25rem;
  padding: 0.65rem 0.25rem;
  font-size: 0.78rem;
  font-weight: 600;
  border-radius: 0.75rem;
  border: 1px solid transparent;
  background: transparent;
  color: var(--text-muted-custom);
  transition: all 0.2s ease;
  min-height: 52px;
}

.mobile-tab-btn i {
  font-size: 1.15rem;
}

.mobile-tab-btn .tab-badge {
  font-weight: 700;
  opacity: 0.85;
}

.mobile-tab-btn.active {
  background: linear-gradient(135deg var(--in-oklab), var(--accent-primary) 0%, var(--accent-primary-hover) 100%);
  color: #ffffff;
  box-shadow: 0 4px 12px rgba(37, 99, 235, 0.25);
}

.mobile-tab-btn.active .tab-badge {
  color: #ffffff;
}

/* Mobile Work Card Styles */
.admin-work-card {
  border: 1px solid var(--border-color-custom) !important;
  background-color: var(--card-bg);
}

.admin-thumb {
  width: 76px;
  height: 76px;
  border: 1px solid var(--border-color-custom);
}

.admin-item-title {
  font-size: 0.94rem;
  line-height: 1.35;
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden;
  word-break: break-word;
  overflow-wrap: break-word;
}

.admin-item-desc {
  font-size: 0.78rem;
  line-height: 1.4;
  display: -webkit-box;
  -webkit-line-clamp: 1;
  -webkit-box-orient: vertical;
  overflow: hidden;
  word-break: break-word;
  overflow-wrap: break-word;
}

/* Custom Action Buttons */
.btn-action-edit {
  background: var(--accent-primary-soft);
  color: var(--accent-primary);
  border: 1px solid var(--accent-primary-border);
  font-size: 0.85rem;
  transition: all 0.2s ease;
}
.btn-action-edit:hover {
  background: var(--accent-primary);
  color: #ffffff;
}

.btn-action-delete {
  background: rgba(239, 68, 68, 0.08);
  color: #dc2626;
  border: 1px solid rgba(239, 68, 68, 0.22);
  font-size: 0.85rem;
  transition: all 0.2s ease;
}
.btn-action-delete:hover {
  background: #dc2626;
  color: #ffffff;
}

/* Touch-friendly inputs: 16px to prevent iOS auto-zoom */
.form-control-touch {
  font-size: 16px !important;
  min-height: 48px;
}

textarea.form-control-touch {
  min-height: 100px;
}

/* Upload Dropzone */
.upload-dropzone {
  border: 2px dashed var(--accent-primary-border);
  background-color: var(--bg-surface-alt);
  transition: all 0.2s ease;
}

.preview-img-container {
  max-width: 260px;
}

.preview-img {
  max-height: 180px;
  object-fit: cover;
  box-shadow: var(--shadow-soft);
}

.cursor-pointer {
  cursor: pointer;
}

.max-w-300 {
  max-width: 300px;
}

.admin-tab-btn {
  color: var(--text-secondary-custom);
  transition: all 0.2s ease;
}
.admin-tab-btn:hover {
  background-color: var(--accent-primary-soft);
  color: var(--accent-primary);
}
.admin-tab-btn.active {
  background: linear-gradient(135deg var(--in-oklab), var(--accent-primary) 0%, var(--accent-primary-hover) 100%) !important;
  color: #ffffff !important;
  box-shadow: 0 4px 14px rgba(37, 99, 235, 0.25);
}
</style>
