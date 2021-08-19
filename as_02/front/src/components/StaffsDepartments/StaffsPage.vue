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
      </div>
      <multiselect select-label="" v-model="newStaff.skills" :options="skills" :multiple="true" :close-on-select="false"
                   :clear-on-select="false" placeholder="Выберите умения" label="name" track-by="name">
        <template slot="selection" slot-scope="{ isOpen }"><span class="multiselect__single"
                                                                 v-if="newStaff.skills.length &amp;&amp; !isOpen">Умения</span>
        </template>
      </multiselect>
      <div @click="CreateWithSkills()" class="buttonAdd">Добавить</div>
    </div>

    <div class="departmentSelector">
      <select v-on:change="GetByDepartmentId($event.target.value)" v-model="selectedDepId" class="depSelect">
        <option value="" selected>Все отделы</option>
        <option v-for="dep in deps" :key="dep.id" :value="dep.id">{{ dep.name }}</option>
      </select>
    </div>
    <div v-for="dep in this.visualDeps" :key="dep.id">
      <div v-for="(staff,index) in dep.staffs" v-bind:key="staff.id" class="staff">
        <div v-if="isUpdateStaff!=staff" class="staffItems">
          <div class="staffItem">{{ staff.id }}</div>
          <div class="staffItem">{{ dep.name }}</div>
          <div class="staffItem">{{ staff.fio }}</div>
          <div class="staffItem">{{ staff.salary }}</div>
        </div>
        <div v-if="isUpdateStaff!=staff" class="skillsList">
          <span  v-for="skill in staff.skills" :key="skill.id">{{skill.name + " "}}</span>
        </div>
        <div v-if="isUpdateStaff!=staff" @click="isUpdateStaff=staff;updateStaff = staff;" class="buttonUpdate">
          Изменить
        </div>
        <div v-if="isUpdateStaff!=staff" class="buttonDelete" @click="DeleteStaff(staff,index)">Удалить</div>
        <div v-if="isUpdateStaff==staff" class="updateItems">
          <div class="updateItem">{{ staff.id }}</div>
          <select v-model="updateStaff.department_id" class="updateItem">
            <option value="" disabled>Выберите отдел</option>
            <option v-for="dep in deps" :key="dep.id" :value="dep.id">{{ dep.name }}</option>
          </select>
          <input v-model="updateStaff.fio" placeholder="ФИО" class="updateItem">
          <input v-model="updateStaff.salary" placeholder="Зарплата" class="updateItem">
        </div>
        <multiselect v-if="isUpdateStaff==staff" v-model="staff.skills" :options="skills" :multiple="true"
                     :close-on-select="false" :clear-on-select="false" placeholder="Выберите умения" label="name"
                     track-by="name">
          <template slot="selection" slot-scope="{ isOpen }"><span class="multiselect__single"
                                                                   v-if="staff.skills.length &amp;&amp; !isOpen">{{
              staff.skills.length
            }} умение(-я)</span></template>
        </multiselect>
        <div v-if="isUpdateStaff==staff" @click="UpdateStaffWithSkills();isUpdateStaff=null;" class="buttonConfirm">
          Сохранить
        </div>
        <div v-if="isUpdateStaff==staff" @click="isUpdateStaff=null" class="buttonBack">Отменить</div>
      </div>
    </div>
  </div>

</template>

<script>

import Multiselect from 'vue-multiselect'
import axios from "axios";

export default {
  name: "StaffsPage",
  components: {
    Multiselect
  },
  data() {
    return {
      allDepsStaffs: [],
      visualDeps: [],
      deps: [],
      selectedDepId: '',
      newStaff: {
        department_id: '',
        fio: '',
        salary: '',
        skills: []
      },
      isUpdateStaff: '',
      updateStaff: '',
    }
  },
  beforeMount() {
    this.GetAllStaffsWithDepartments();
    this.GetAllDepartments();
    this.GetAllSkills();

  },
  methods: {
    GetAllStaffsWithDepartments() {
      axios.get('https://localhost:44390/staffs/GetAllByDepartmentsWithSkills'
      ).then(response => {
        this.allDepStaffs = response.data;
        this.visualDeps = response.data;
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
    GetAllSkills() {
      axios.get('https://localhost:44390/skills/GetAll'
      ).then(response => {
        this.skills = response.data;
        console.log(response.data);
      }).catch((error) => {
        console.log(error);
      })
    },
    GetByDepartmentId(id) {
      if (id == "") {
        this.GetAllStaffsWithDepartments();
        return;
      }
      this.allDepStaffs.forEach(dep => {
        if (dep.id == id) {
          this.visualDeps = [];
          this.visualDeps[0] = dep;
        }
      });
    },
    DeleteStaff(staff, index) {
      axios.delete('https://localhost:44390/staffs/delete/' + staff.id
      ).then(response => {
        console.log(staff);
        console.log(index);
        this.visualDeps.forEach(function (dep) {
          if (dep.id == staff.department_id) {
            dep.staffs.splice(index, 1);
            console.log(dep);
          }
        })
        console.log(this.visualDeps);
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
        this.visualDeps.forEach(function (dep) {
          if (dep.id == response.data.department_id) {
            dep.staffs.push(response.data);
          }
        })
        console.log(response.data);
      }).catch((error) => {
        console.log(error);
      })
    },
    CreateWithSkills() {
      if (!this.newStaff.department_id || !this.newStaff.salary || !this.newStaff.fio) {
        alert("Заполните все данные");
        return;
      }
      this.newStaff.salary = parseInt(this.newStaff.salary);
      axios.post('https://localhost:44390/staffs/CreateWithSkills', this.newStaff
      ).then(response => {
        this.visualDeps.forEach(function (dep) {
          if (dep.id == response.data.department_id) {
            dep.staffs.push(response.data);
          }
        })
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
    UpdateStaffWithSkills() {
      console.log(this.updateStaff);

      if (!this.updateStaff.department_id || !this.updateStaff.salary || !this.updateStaff.fio) {
        alert("Заполните все данные");
        return;
      }
      this.updateStaff.salary = parseInt(this.updateStaff.salary);
      axios.put('https://localhost:44390/staffs/UpdateWithSkills', this.updateStaff
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
@import "../../../node_modules/vue-multiselect/dist/vue-multiselect.min.css";

.StaffsPage {
  font-family: 'Roboto', sans-serif;
  margin-left: auto;
  margin-right: auto;
  box-sizing: border-box;
  width: 1200px;
  font-size: 14pt;

  .depSelect {
    padding-left: 1%;
    margin-bottom: auto;
    font-size: 14pt;
    display: inline-flex;
    align-items: center;
    justify-content: center;
    text-align: center;
    box-sizing: border-box;
    outline: none;
    width: 100%;
    margin-top: 15px;
    height: 50px;
    border: 1px solid black;
  }

  .staff, .newStaff {
    display: flex;
    text-align: center;
    border-radius: 0;
    width: 100%;
    margin-top: 10px;
    height: 50px;
    border: 1px solid black;
    box-sizing: border-box;

    .multiselect, .skillsList {
      margin-top: auto;
      margin-bottom: auto;
      width: 20%;
      font-size: 12pt;
    }

    .updateItems, .newItems, .staffItems {
      margin-top: auto;
      margin-bottom: auto;
      display: flex;
      text-align: center;
      border-radius: 0;
      width: 50%;
      display: inline-flex;
      align-items: center;
      justify-content: left;
      text-align: left;
      box-sizing: border-box;
      height: 50px;

      :first-child {
        width: 5%;
        margin-left: 3%;
      }

      :nth-child(2) {
        width: 20%;
      }

      :nth-child(3) {
        margin-left: 5%;
        width: 40%;
        margin-right: 5%;

      }

      :nth-child(4) {
        width: 15%;
      }


      .newItem:nth-child(2), .newItem:nth-child(3), .newItem:nth-child(4), .updateItem:nth-child(2), .updateItem:nth-child(3), .updateItem:nth-child(4) {
        border-bottom: 1px solid black;
      }


      .updateItem, .newItem, .staffItem {
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
    }

    .buttonConfirm, .buttonBack, .buttonUpdate, .buttonDelete, .buttonAdd {
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
  }
}


</style>