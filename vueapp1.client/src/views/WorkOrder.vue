<template>
  <div>
    <h2>📋 Work Order Page</h2>

    <CInputGroup class="mb-3">
      <CFormInput placeholder="OrderNumber" v-model="newOrder.orderNumber" />
      <CFormInput placeholder="ProductNumber" v-model="newOrder.productNumber" />
      <CFormInput placeholder="Assigned To" v-model="newOrder.assignedTo" />
      <select class="form-select" v-model="newOrder.status">
        <option disabled value="">Status</option>
        <option v-for="opt in statusOptions" :key="opt.value" :value="opt.value">
          {{ opt.label }}
        </option>
      </select>
      <CFormInput type="datetime-local" placeholder="Created At (yyyy-mm-dd)" v-model="newOrder.createdAt" />
      <CFormInput type="datetime-local" placeholder="Completed At (yyyy-mm-dd)" v-model="newOrder.completedAt" />

    </CInputGroup>

    <CButton color="primary" @click="submitOrder">送出工單</CButton>

    <CTable>
      <CTableHead>
        <CTableRow>
          <CTableHeaderCell scope="col">ID</CTableHeaderCell>
          <CTableHeaderCell scope="col">OrderNumber</CTableHeaderCell>
          <CTableHeaderCell scope="col">ProductNumber</CTableHeaderCell>
          <CTableHeaderCell scope="col">Assigned To</CTableHeaderCell>
          <CTableHeaderCell scope="col">Status</CTableHeaderCell>
          <CTableHeaderCell scope="col">Create At</CTableHeaderCell>
          <CTableHeaderCell scope="col">Completed At</CTableHeaderCell>
          <CTableHeaderCell scope="col">Edit</CTableHeaderCell>
          <CTableHeaderCell scope="col">Delete</CTableHeaderCell>
        </CTableRow>
      </CTableHead>
      <CTableBody>
        <CTableRow v-for="item in workOrder" :key="item.id">
          <CTableHeaderCell scope="row">{{item.id}}</CTableHeaderCell>
          <CTableDataCell>{{item.orderNumber}}</CTableDataCell>
          <CTableDataCell>{{item.productNumber}}</CTableDataCell>
          <CTableDataCell>{{item.assignedTo}}</CTableDataCell>
          <CTableDataCell>{{item.status}}</CTableDataCell>
          <CTableDataCell>{{item.createdAt}}</CTableDataCell>
          <CTableDataCell>{{item.completedAt}}</CTableDataCell>
          <CTableDataCell>
            <CButton color="primary" @click="openEditModal(item)">編輯</CButton>
          </CTableDataCell>
          <CTableDataCell>
            <CButton color="danger" @click="deleteWorkOrder(item)">刪除</CButton>
          </CTableDataCell>
        </CTableRow>

      </CTableBody>
    </CTable>
    <CModal :visible="visibleEditModal"
            @close="() => { visibleEditModal = false }"
            aria-labelledby="LiveDemoExampleLabel">
      <CModalHeader>
        <CModalTitle id="LiveDemoExampleLabel">編輯工單</CModalTitle>
      </CModalHeader>
      <div class="ModalBox">
      <CInputGroup class="mb-3" v-if="editedOrder">
        <CInputGroupText id="basic-addon1">OrderNumber</CInputGroupText>
        <CFormInput placeholder="OrderNumber" v-model="editedOrder.orderNumber" />
      </CInputGroup>
      <CInputGroup class="mb-3" v-if="editedOrder">
        <CInputGroupText id="basic-addon1">ProductNumber</CInputGroupText>
        <CFormInput placeholder="ProductNumber" v-model="editedOrder.productNumber" />
      </CInputGroup>
      <CInputGroup class="mb-3" v-if="editedOrder">
        <CInputGroupText id="basic-addon1">Assigned To</CInputGroupText>
        <CFormInput placeholder="Assigned To" v-model="editedOrder.assignedTo" />
      </CInputGroup>
      <CInputGroup class="mb-3" v-if="editedOrder">
        <select class="form-select" v-model="editedOrder.status">
          <option disabled value="">Status</option>
          <option v-for="opt in statusOptions" :key="opt.value" :value="opt.value">
            {{ opt.label }}
          </option>
        </select>
      </CInputGroup>
      <CInputGroup class="mb-3" v-if="editedOrder">
        <CInputGroupText id="basic-addon1">Created At</CInputGroupText>
        <CFormInput type="datetime-local" placeholder="Created At (yyyy-mm-dd)" v-model="editedOrder.createdAt" />
      </CInputGroup>
      <CInputGroup class="mb-3" v-if="editedOrder">
        <CInputGroupText id="basic-addon1">Completed At</CInputGroupText>
        <CFormInput  type="datetime-local" placeholder="Completed At (yyyy-mm-dd)" v-model="editedOrder.completedAt" />
      </CInputGroup>
        </div>
        <CModalFooter>
          <CButton color="secondary" @click="() => {visibleEditModal = false}">
            Close
          </CButton>
          <CButton color="primary" @click="saveEdit">Save changes</CButton>
        </CModalFooter>
    </CModal>
  </div>
</template>
<script setup>
  import { ref, onMounted } from 'vue'
  import axios from 'axios'
  const workOrder = ref([])
  const newOrder = ref({
    orderNumber: '',
    productNumber: '',
    assignedTo: '',
    createdAt: '',
    completedAt: ''
  })
  const statusOptions = [
    { label: 'Draft', value: 'Draft' },
    { label: 'Pending', value: 'Pending' },
    { label: 'Scheduled', value: 'Scheduled' },
    { label: 'Released', value: 'Released' },
    { label: 'InProgress', value: 'InProgress' },
    { label: 'OnHold', value: 'OnHold' },
    { label: 'Completed', value: 'Completed' },
    { label: 'Closed', value: 'Closed' },
    { label: 'Cancelled', value: 'Cancelled' },
  ]

  const visibleEditModal = ref(false)
  const editedOrder = ref(null)
  onMounted(async () => {
    try {
      const res = await fetch('/api/WorkOrders')
      const data = await res.json()
      workOrder.value = data
    } catch (error) {
      console.error('取得工單失敗', error)
    }
  })
  const openEditModal = (item) => {
    editedOrder.value = { ...item }
    visibleEditModal.value = true
  }
  const submitOrder = async () => {
    try {
      const res = await axios.post('/api/WorkOrders', newOrder.value)
      workOrder.value.push(res.data)
      // 清空欄位
      newOrder.value = {
        orderNumber: '',
        productNumber: '',
        assignedTo: '',
        status: '',
        createdAt: '',
        completedAt: ''
      }
      newOrder
    } catch (err) {
      console.log('新增工單失敗',err)
    }
  }
  const deleteWorkOrder = async(item) => {
    await axios.delete(`/api/workorders/${item.id}`)
    workOrder.value = workOrder.value.filter(w => w.id !== item.id)
  }
  const saveEdit = async () => {
    try {
      await axios.put(`/api/WorkOrders/${editedOrder.value.id}`, editedOrder.value)
      const index = workOrder.value.findIndex(w => w.id === editedOrder.value.id)
      if (index !== -1) {
        workOrder.value[index] = { ...editedOrder.value }

      }
      visibleEditModal.value = false
    } catch (error) {
      console.log("編輯工單失敗",error)
    }
  }


  </script>
<style>
  .ModalBox {
    margin: 20px;
  }
</style>
