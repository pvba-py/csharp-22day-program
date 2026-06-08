<script setup>
import { ref, onMounted } from 'vue'

defineEmits(['go-dashboard'])

const patients = ref([])
const searchCity = ref('')
const searchName = ref('')
const isSearching = ref(false)

const loadPatients = async () => {
  isSearching.value = true
  const cityQuery = searchCity.value ? `&city=${searchCity.value}` : ''
  const nameQuery = searchName.value ? `&name=${searchName.value}` : ''
  const response = await fetch(`https://localhost:7006/api/patients/search?activeOnly=true${cityQuery}${nameQuery}`)
  
  patients.value = await response.json()
  isSearching.value = false
}

onMounted(() => {
  loadPatients()
})
</script>

<template>
  <h1>CareBridge Patients</h1>

  <button @click="$emit('go-dashboard')">Go to Dashboard</button>

  <div style="margin-bottom: 20px;">
    <input type="text" v-model="searchCity" placeholder="Search by city..." />
    <input type="text" v-model="searchName" placeholder="Search by name..." />
    <button class="btn btn-primary" style="margin-left: 10px;" @click="loadPatients" :disabled="isSearching">
      {{ isSearching ? 'Searching...' : 'Search' }}
    </button>
  </div>

 <h3> Showing Patients Count:
  {{ patients.length }}
 </h3>

<table class="table">
  <thead>
    <tr>
      <th>Patient Id</th>
      <th>Full Name</th>
      <th>City</th>
      <th>Status</th>
    </tr>
  </thead>

  <tbody>
    <tr v-for="p in patients" :key="p.patientId">
      <td>{{ p.patientId }}</td>
      <td>{{ p.fullName }}</td>
      <td>{{ p.city }}</td>
      <td>
        <span
          :class="p.isActive ? 'text-success fw-bold' : 'text-danger fw-bold'"
        >IsActive
        </span>
      </td>
    </tr>
  </tbody>
</table>
</template>
