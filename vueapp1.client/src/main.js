import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import CoreuiVue from '@coreui/vue'
import { CForm, CFormInput, CTable, CTableHead, CTableBody, CTableRow, CTableHeaderCell } from '@coreui/vue' // （可選）
import '@coreui/coreui/dist/css/coreui.min.css'
const app = createApp(App)
app.use(CoreuiVue)
app.use(router)
app.mount('#app')
