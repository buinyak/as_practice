<template>
  <div class="DepartmentsPage">
    <div class="departmentsHeaders">
      <div class="departmentsHeader">ID</div>
      <div class="departmentsHeader">Название отдела</div>
    </div>
    <div v-for="dep in departments" v-bind:key="dep.id" class="departmentItems">
      <div class="departmentItem">{{dep.id}}</div>
      <div class="departmentItem">{{dep.name}}</div>
    </div>
  </div>
</template>

<script>

import axios from "axios";

export default {
  name: "DepartmentsPage",
  props: [],
  data() {
    return {
      departments:{},
    }
  },
  beforeMount() {
    this.getAllDepartments();
  },
  methods: {
    getAllDepartments() {
      axios.get('https://localhost:44390/departments/getall'
      ).then(response => {
        this.departments = response.data;
        console.log(response.data);
      }).catch((error) => {
        console.log(error);
      })
    },
  }
}
</script>

<style scoped lang="scss">
.DepartmentsPage{
  font-family: 'Roboto', sans-serif;
  margin-left: auto;
  margin-right: auto;
  box-sizing: border-box;
  width: 400px;
  font-size:14pt;
  .departmentsHeaders{
    box-sizing: border-box;
    display: flex;
    text-align: center;
    .departmentsHeader{
      display: inline-flex;
      align-items: center;
      justify-content: left;
      text-align: left;
    }:first-child{
       width: 20%;
       margin-left: 5%;
     }
    :nth-child(2){
      width: 80%;
    }
  }
  .departmentItems{
    display: flex;
    text-align: center;
    border-radius: 0;
    border: 1px solid black;
    margin-top: 10px;
    .departmentItem{
      height: 50px;
      display: inline-flex;
      align-items: center;
      justify-content: left;
      text-align: left;

    }:first-child{
       width: 20%;
       margin-left: 5%;
     }
    :nth-child(2){
      width: 80%;
    }
  }
}

</style>