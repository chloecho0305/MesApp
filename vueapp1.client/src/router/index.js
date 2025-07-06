import { createRouter, createWebHistory } from 'vue-router'
import WorkOrderView from '../views/WorkOrder.vue'


const routes = [
  {
    path: '/workorder',
    name: 'WorkOrder',
    component: WorkOrderView
  },
]
const router = createRouter({
  history: createWebHistory(),
  routes
  
})

export default router
