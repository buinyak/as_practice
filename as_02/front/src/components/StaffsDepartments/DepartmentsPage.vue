<template>
  <div class="DepartmentsPage">
    <div clas="newDep">
      <div class="newItems">
        <div class="newItem">ID</div>
        <input v-model="newDep.name" placeholder="Название отдела" class="newItem">
        <div class="newItem">Ср. Зарплата</div>
        <div @click="CreateDep()" class="buttonAdd">Добавить</div>
      </div>
    </div>
    <div v-for="(dep,index) in departments" v-bind:key="dep.id">
      <div v-if="isUpdateDep!=dep" class="departmentItems">
        <div class="departmentItem">{{ dep.id }}</div>
        <div class="departmentItem">{{ dep.name }}</div>
        <div class="departmentItem">{{midSalaries[index].MidSalary}}</div>
        <div @click="isUpdateDep=dep;updateDep = dep;" class="buttonUpdate">Изменить</div>


        <div @click="DeleteDep(dep,index)" class="buttonDelete">Удалить</div>
      </div>
      <div v-if="isUpdateDep==dep" class="updateItems">
        <div class="updateItem">{{ dep.id }}</div>
        <input v-model="updateDep.name" placeholder="Название отдела" class="updateItem">
        <div class="departmentItem">{{midSalaries[index].MidSalary}}</div>
        <div @click="UpdateDep();isUpdateDep=null;" class="buttonConfirm">Сохранить</div>
        <div @click="isUpdateDep=null" class="buttonBack">Отменить</div>
        <div class="updateItem"></div>
      </div>
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
      departments: {},
      newDep: {},
      updateDep: {},
      isUpdateDep: {},
      midSalaries: {},
    }
  },
  beforeMount() {
    this.GetMidSalaries();
    this.GetAllDepartments();
  },
  methods: {
    GetAllDepartments() {
      axios.get('https://localhost:44390/departments/getall'
      ).then(response => {
        this.departments = response.data;
        console.log(response.data);
      }).catch((error) => {
        console.log(error);
      })
    },
    DeleteDep(dep, index) {
      axios.delete('https://localhost:44390/departments/delete/' + dep.id
      ).then(response => {
        this.departments.splice(index,1);
        console.log(response.data);
      }).catch((error) => {
        console.log(error);
      })
    },
    CreateDep() {
      if (!this.newDep.name) {
        alert("Заполните все данные");
        return;
      }
      axios.post('https://localhost:44390/departments/create', this.newDep
      ).then(response => {
        this.departments.push(response.data);
        console.log(response.data);
      }).catch((error) => {
        console.log(error);
      })
    },
    GetMidSalaries() {
      axios.get('https://localhost:44390/departments/GetMidSalaries'
      ).then(response => {
        this.midSalaries = response.data;
      }).catch((error) => {
        console.log(error);
      })
    },
    UpdateDep() {
      console.log(this.updateDep);
      if (!this.updateDep.name) {
        alert("Заполните все данные");
        return;
      }
      axios.put('https://localhost:44390/departments/update', this.updateDep
      ).then(response => {
        console.log(response.data);
      }).catch((error) => {
        console.log(error);
      })
    },
  }
}
</script>

<style scoped lang="scss">
.DepartmentsPage {
  font-family: 'Roboto', sans-serif;
  margin-left: auto;
  margin-right: auto;
  box-sizing: border-box;
  width: 700px;
  font-size: 14pt;

  .newItems, .updateItems, .departmentItems {
    height: 50px;
    display: flex;
    text-align: center;
    border-radius: 0;
    border: 1px solid black;
    margin-top: 10px;

    .newItem, .updateItem, .departmentItem {
      margin-bottom: auto;
      margin-top: auto;
      display: inline-flex;
      align-items: center;
      justify-content: left;
      text-align: left;
      padding: 0;
      outline: none;
      border: 0;
      font-size: 14pt;
      box-sizing: border-box;

    }

    :first-child {
      width: 5%;
      margin-left: 4%;
    }

    :nth-child(2) {
      width: 22%;
      margin-left: 2%;
      margin-right: 5%
    }
    :nth-child(3) {
      width: 20%;
    }

    .newItem:nth-child(2), .updateItem:nth-child(2) {
      border-bottom: 1px solid black;
    }


    .buttonAdd, .buttonConfirm, .buttonBack, .buttonUpdate, .buttonDelete {
      margin: auto;
      height: 100%;
      cursor: pointer;
      width: 18%;
      box-sizing: border-box;
      border-radius: 0;
      border: 1px solid black;
      height: 50%;
      background-color: white;
      text-align: center;
      vertical-align: middle;
    }

    .buttonAdd {
      width: 25%;
    }

  }
}

</style>