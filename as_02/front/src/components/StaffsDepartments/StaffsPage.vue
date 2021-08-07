<template>
  <div class="StaffsPage">
    <div class="staffsHeaders">
      <div class="staffsHeader">ID</div>
      <div class="staffsHeader">Название отдела</div>
      <div class="staffsHeader">ФИО</div>
      <div class="staffsHeader">Зарплата</div>
    </div>
    <div clas="newStaff">
      <div class="newItems">
        <input placeholder="ID" disabled="true" class="newItem">
        <select v-model="newStaff.department_id" class="newItem">
          <option value="" disabled selected>Выберите отдел</option>
          <option v-for="dep in deps" :key="dep.id" :value="dep.id">{{ dep.name }}</option>
        </select>
        <input v-model="newStaff.fio" placeholder="ФИО" class="newItem">
        <input v-model="newStaff.salary" placeholder="Зарплата" class="newItem">
        <div @click="CreateStaff()" class="buttonAdd">Добавить</div>
      </div>
    </div>
    <div v-for="(staff,index) in staffs" v-bind:key="staff.id" class="staffItems">
      <div class="staffItem">{{ staff.id }}</div>
      <div class="staffItem">{{ staff.department_name }}</div>
      <div class="staffItem">{{ staff.fio }}</div>
      <div class="staffItem">{{ staff.salary }}</div>
      <div class="buttonUpdate">Изменить</div>
      <div class="buttonDelete" @click="DeleteStaff(staff,index)">Удалить</div>

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
      newStaff: {
        department_id: '',
        fio: '',
        salary: '',
      }
    }
  },
  beforeMount() {
    this.GetAllStaffsWithDepartments();
    this.GetAllDepartments();

  },
  methods: {
    onChange: function (event) {
      let index = event.target.selectedIndex // this selectedIndex is from event.
      this.selectedIndex = index
    },
    GetAllStaffsWithDepartments() {
      axios.get('https://localhost:44390/departments/GetAllStaffsWithDepartments'
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
    DeleteStaff(staff,index) {
      axios.delete('https://localhost:44390/staffs/delete/'+staff.id
      ).then(response => {
        this.staffs.splice(index);
        console.log(response.data);
      }).catch((error) => {
        console.log(error);
      })
    },
    CreateStaff() {
      if(!this.newStaff.department_id || !this.newStaff.salary || !this.newStaff.fio){
        alert("Заполните все данные");
        return;
      }
      this.newStaff.salary = parseInt(this.newStaff.salary);
      axios.post('https://localhost:44390/staffs/create', this.newStaff
      ).then(response => {
        this.staffs.push(response.data);
        console.log(response.data);
        this.GetAllStaffsWithDepartments()
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

  .staffsHeaders {
    box-sizing: border-box;
    display: flex;
    text-align: center;

    .staffsHeader {
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
      width: 15%;
    }

  }

  .newItems {
    display: flex;
    text-align: center;
    border-radius: 0;
    border: 1px solid black;
    margin-top: 10px;
    height: 50px;

    .buttonAdd {
      margin: auto;
      height: 100%;
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

    .newItem {
      font-size: 14pt;
      border: 0;
      display: inline-flex;
      align-items: center;
      justify-content: left;
      text-align: left;
      box-sizing: border-box;
      margin: 0;
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
      width: 30%;
    }

    :nth-child(4) {
      width: 12%;
    }
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