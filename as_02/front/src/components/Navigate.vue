<template>
  <div class="Navigate">
    <div class="NavigatePanel">
      <div :class="{ activeButtonNav : activeNav==='ruFiles'}" @click="$router.push('/ruFiles').catch(()=>{});activeNav='ruFiles';" class="buttonNav" style="border-left:0">Russian
    </div>
    <div :class="{ activeButtonNav : activeNav==='engFiles'}" @click="$router.push('/engFiles').catch(()=>{});activeNav='engFiles';" class="buttonNav">English</div>
    <div :class="{ activeButtonNav : activeNav==='digitFiles'}" @click="$router.push('/digitFiles').catch(()=>{});activeNav='digitFiles';" class="buttonNav">Digital</div>
  </div>
  <FileRedactor :activeNav="activeNav"></FileRedactor>
  </div>
</template>

<script>
import FileRedactor from "@/components/FileRedactor";

export default {
  name: "Navigate",
  components: {FileRedactor},
  props: {},
  data() {
    return {
      activeNav:"ruFiles",
    }
  },
  methods: {

  }
}

</script>

<style scoped lang="scss">
.NavigatePanel {
  font-family: 'Roboto', sans-serif;
  font-weight: 300;
  height: 50px;
  width: 800px;
  margin-left: auto;
  margin-right: auto;
  box-sizing: border-box;
  border-radius: 0;
  border: 1px solid black;
  display: flex;
  text-align: center;
  border-bottom: 0;

  .activeButtonNav{
    background-color: #b9b9b9;
  }
  .buttonNav {
    height: 100%;
    cursor: pointer;
    width: 100%;
    box-sizing: border-box;
    border-radius: 0;
    border-left: 1px solid black;
    display: inline-flex;
    align-items: center;
    justify-content: center;
    text-align: center;

    &:hover {
      background-color: #b9b9b9;
    }
  }
}

</style>