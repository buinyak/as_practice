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
      <select v-on:change="GetByDepartmentId($event.target.value)" class="depSelect">
        <option value="" disabled selected>Отфильтровать по отделу</option>
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
      selectedDep:'',
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
      axios.get('https://localhost:44390/departments/GetAllStaffsWithDepartments'
      ).then(response => {
        this.staffs = response.data;
        this.AllStaffs = response.data;
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
        this.staffs.splice(index);
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
        this.staffs.push(response.data);
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

  .updateItems, .newItems {
    display: flex;
    text-align: center;
    border-radius: 0;
    border: 1px solid black;
    margin-top: 10px;
    height: 50px;

    .buttonAdd {
      margin: auto;
      cursor: pointer;
      width: 20%;
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

    .buttonConfirm, .buttonBack {
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

    .updateItem, .newItem {
      margin-top: auto;
      margin-bottom: auto;
      border-bottom: 1px solid black;
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
      border-bottom: 0;
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
  }

}

.departmentSelector{
  display: flex;
  text-align: center;
  border-radius: 0;
  border: 1px solid black;
  margin-top: 10px;
  height: 50px;
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
.staffItems {
  display: flex;
  text-align: center;
  border-radius: 0;
  border: 1px solid black;
  margin-top: 10px;


  .buttonUpdate, .buttonDelete {
    margin: auto;
    height: 100%;
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


  .staffItem {
    height: 50px;
    display: inline-flex;
    align-items: center;
    justify-content: left;
    text-align: left;

  }

  :first-child {
    width: 5%;
    margin-left: 3%;
  }

  :nth-child(2) {
    width: 20%;
  }

  :nth-child(3) {
    width: 30%;
  }

  :nth-child(4) {
    width: 12%;
  }
}


</style>