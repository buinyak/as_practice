<template>
  <div class="StaffsPage">
    <div class="newStaff">
      <div class="newItems">
        <div class="newItem">ID</div>
        <select v-model="newStaff.department_id" class="newItem">
          <option value="" disabled selected>Отдел</option>
          <option v-for="dep in deps" :key="dep.id" :value="dep.id">{{ dep.name }}</option>
        </select>
        <input v-model="newStaff.fio" placeholder="ФИО" class="newItem">
        <input v-model="newStaff.salary" placeholder="Зарплата" class="newItem">
        <div @click="CreateStaff()" class="buttonAdd">Добавить</div>
      </div>
    </div>
    <div class="departmentSelector">
      <select v-on:change="GetByDepartmentId($event.target.value)" v-model="selectedDepId" class="depSelect">
        <option value="" selected>Все отделы</option>
        <option v-for="dep in deps" :key="dep.id" :value="dep.id">{{ dep.name }}</option>
      </select>
    </div>
    <div v-for="(staff,index) in staffs" v-bind:key="staff.id">
      <div v-if="isUpdateStaff!=staff" class="staffItems">
        <div class="staffItem">{{ staff.id }}</div>
        <div class="staffItem">{{ staff.department_name }}</div>
        <div class="staffItem">{{ staff.fio }}</div>
        <div class="staffItem">{{ staff.salary }}</div>
        <div @click="isUpdateStaff=staff;updateStaff = staff;" class="buttonUpdate">Изменить</div>
        <div class="buttonDelete" @click="DeleteStaff(staff,index)">Удалить</div>
      </div>
      <div v-if="isUpdateStaff==staff" class="updateItems">
        <div class="updateItem">{{ staff.id }}</div>
        <select v-model="updateStaff.department_id" class="updateItem">
          <option value="" disabled selected>Выберите отдел</option>
          <option v-for="dep in deps" :key="dep.id" :value="dep.id">{{ dep.name }}</option>
        </select>
        <input v-model="updateStaff.fio" placeholder="ФИО" class="updateItem">
        <input v-model="updateStaff.salary" placeholder="Зарплата" class="updateItem">
        <div @click="UpdateStaff();isUpdateStaff=null;" class="buttonConfirm">Сохранить</div>
        <div @click="isUpdateStaff=null" class="buttonBack">Отменить</div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "StaffsPage",
  data() {
    return {
      staffs: [],
      deps: [],
      selectedDepId:'',
      newStaff: {
        department_id: '',
        fio: '',
        salary: '',
      },
      isUpdateStaff: '',
      updateStaff: '',
    }
  },
  beforeMount() {
    this.GetAllStaffsWithDepartments();
    this.GetAllDepartments();

  },
  methods: {
    GetAllStaffsWithDepartments() {
      axios.get('https://localhost:44390/staffs/GetAllStaffsWithDepartments'
      ).then(response => {
        this.staffs = response.data;
        console.log(response.data);
      }).catch((error) => {
        console.log(error);
      })
    },
    GetAllDepartments() {
      axios.get('https://localhost:44390/departments/GetAll'
      ).then(response => {
        this.deps = response.data;
        console.log(response.data);
      }).catch((error) => {
        console.log(error);
      })
    },
    GetByDepartmentId(id) {
      if(id == ""){
        this.GetAllStaffsWithDepartments();
        return;
      }
      axios.get('https://localhost:44390/staffs/GetByDepartmentId/'+id
      ).then(response => {
        this.staffs = response.data;
        console.log(response.data);
      }).catch((error) => {
        console.log(error);
      })
    },
    DeleteStaff(staff, index) {
      axios.delete('https://localhost:44390/staffs/delete/' + staff.id
      ).then(response => {
        this.staffs.splice(index,1);
        console.log(response.data);
      }).catch((error) => {
        console.log(error);
      })
    },
    CreateStaff() {
      if (!this.newStaff.department_id || !this.newStaff.salary || !this.newStaff.fio) {
        alert("Заполните все данные");
        return;
      }
      this.newStaff.salary = parseInt(this.newStaff.salary);
      axios.post('https://localhost:44390/staffs/create', this.newStaff
      ).then(response => {
        this.deps.forEach(function(dep){
          if(dep.id == response.data.department_id){
            response.data.department_name = dep.name;
          }
        })
        if(this.selectedDepId == response.data.department_id || this.selectedDepId == "" ){
          this.staffs.push(response.data);
        }
        console.log(response.data);
      }).catch((error) => {
        console.log(error);
      })
    },
    UpdateStaff() {
      console.log(this.updateStaff);

      if (!this.updateStaff.department_id || !this.updateStaff.salary || !this.updateStaff.fio) {
        alert("Заполните все данные");
        return;
      }
      this.updateStaff.salary = parseInt(this.updateStaff.salary);
      axios.put('https://localhost:44390/staffs/update', this.updateStaff
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
.StaffsPage {
  font-family: 'Roboto', sans-serif;
  margin-left: auto;
  margin-right: auto;
  box-sizing: border-box;
  width: 900px;
  font-size: 14pt;

  .updateItems, .newItems,.staffItems,.departmentSelector {
    display: flex;
    text-align: center;
    border-radius: 0;
    border: 1px solid black;
    margin-top: 10px;
    height: 50px;

    .buttonConfirm, .buttonBack,.buttonUpdate, .buttonDelete,.buttonAdd {
      margin: auto;
      cursor: pointer;
      width: 13%;
      box-sizing: border-box;
      border-radius: 0;
      border: 1px solid black;
      height: 50%;
      background-color: white;
      text-align: center;
      vertical-align: middle;

      &:hover {
        background-color: #b9b9b9;
      }

    }
    .buttonAdd {
      width: 20%;
    }
    .updateItem, .newItem,.staffItem {
      margin-top: auto;
      margin-bottom: auto;
      border-bottom: 0;
      border-top: 0;
      border-left: 0;
      border-right: 0;
      font-size: 14pt;
      display: inline-flex;
      align-items: center;
      justify-content: left;
      text-align: left;
      box-sizing: border-box;
      padding: 0;
      outline: none;
    }

    :first-child {
      width: 4.5%;
      margin-left: 3%;
    }
    :nth-child(2) {

      width: 18%;
    }
    :nth-child(3) {
      margin-left: 2.5%;
      width: 27%;
      margin-right: 3%;
    }
    :nth-child(4) {
      width: 9%;
      margin-right: 3%;
    }
    .newItem:nth-child(2),.newItem:nth-child(3),.newItem:nth-child(4), .updateItem:nth-child(2),.updateItem:nth-child(3),.updateItem:nth-child(4) {
      border-bottom: 1px solid black;
    }
    .depSelect{
      margin-top: auto;
      margin-bottom: auto;
      border: 0;
      font-size: 14pt;
      display: inline-flex;
      align-items: center;
      justify-content: center;
      text-align: center;
      box-sizing: border-box;
      margin-left: 2%;
      padding: 0;
      outline: none;
      width: 100%;
    }

  }

}




</style>