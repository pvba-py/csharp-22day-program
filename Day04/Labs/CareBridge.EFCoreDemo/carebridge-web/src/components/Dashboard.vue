<script setup>
import { ref, onMounted, computed } from 'vue'

const departments = ref([])
const fromDate = ref('') 

const loadData = async () => {
  let url = 'https://localhost:7006/api/analytics/department-load'
  if (fromDate.value) {
    url += `?fromDate=${fromDate.value}`
  }

  const response = await fetch(url)
  departments.value = await response.json()
}

const sum = (key) => computed(() => departments.value.reduce((s, d) => s + d[key], 0))

const grandTotal = sum('total')

onMounted(() => {
  loadData()
})
</script>

<template>
  <h1>Department Encounter Dashboard</h1>

  <div style="margin-bottom: 20px;">
    <label>From Date: </label>
    <input type="date" v-model="fromDate" />
    <button @click="loadData">Filter</button>
  </div>

  <table border="1" cellpadding="5" style="border-collapse: collapse;">
    <tr>
      <th>Department</th>
      <th>Inpatient</th>
      <th>Outpatient</th>
      <th>ED</th>
      <th>Total</th>
    </tr>

    <tr 
      v-for="(dept, index) in departments" 
      :key="dept.departmentName"
      :style="index === 0 ? 'background-color: yellow; font-weight: bold;' : ''"
    >
      <td>{{ dept.departmentName }}</td>
      <td>{{ dept.inpatient }}</td>
      <td>{{ dept.outpatient }}</td>
      <td>{{ dept.ed }}</td>
      <td>{{ dept.total }}</td>
    </tr>

    <h3 style="font-weight: bold; text-align: center; background-color: #f0f0f0;">
      <td>Grand Total</td>
      <td>{{ grandTotal }}</td>
    </h3>
  </table>
</template>