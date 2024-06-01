<script setup>
import Navigator from './components/Navigator.vue'
import { RouterView } from 'vue-router';
</script>

<template>
  <Navigator />
  <main>
    <RouterView />
  </main>
</template>