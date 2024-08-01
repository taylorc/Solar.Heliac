<script setup lang="ts">
const route = useRoute()

const { data, status, error, refresh, clear } = await useFetch("https://localhost:7255/api/heroes")

</script>

<template>
  <div>
    {{ status }}
    {{ error }}
    <p v-if="status=='pending'">Fetching...</p>
    <pre v-else-if="error">Could not load data: {{ error.data }}</pre>
    <ul>
      <li v-for="hero in data" 
        :key="hero.id" 
        > {{ hero }} - {{ hero.powers[0].name }}</li>
    </ul>
  </div>
</template>