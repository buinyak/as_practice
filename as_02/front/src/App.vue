<template>
  <div id="app">
    <FileRedactor/>
  </div>
</template>

<script>
import FileRedactor from './components/FileRedactor.vue'

export default {
  name: 'App',
  components: {
    FileRedactor
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
