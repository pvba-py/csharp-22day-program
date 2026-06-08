<template>
  <Main v-if="view === 'main'" @go-dashboard="view = 'dashboard'" />
  <Dashboard v-else />
</template>

<script setup lang="ts">
import { ref } from 'vue'
import Main from './components/Main.vue'
import Dashboard from './components/Dashboard.vue'

const view = ref<'main' | 'dashboard'>('main')
</script>