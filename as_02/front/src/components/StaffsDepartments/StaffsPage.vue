<template>
  <div class="StaffsPage">
    <div class="staffsHeaders">
      <div class="staffsHeader">ID</div>
      <div class="staffsHeader">Название отдела</div>
      <div class="staffsHeader">ФИО</div>
      <div class="staffsHeader">Зарплата</div>
    </div>
    <div v-for="staff in staffs" v-bind:key="staff.id" class="staffItems">
      <div class="staffItem">{{ staff.id }}</div>
      <div class="staffItem">{{ staff.department_name }}</div>
      <div class="staffItem">{{ staff.fio }}</div>
      <div class="staffItem">{{ staff.salary }}</div>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "StaffsPage",
  data() {
    return {
      staffs: {},
    }
  },
  beforeMount() {
    this.getAllStaffs();
  },
  methods: {
    getAllStaffs() {
      axios.get('https://localhost:44390/departments/GetAllStaffsWithDepartments'
      ).then(response => {
        this.staffs = response.data;
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
  width: 700px;
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
      width: 10%;
      margin-left: 5%;
    }

    :nth-child(2) {
      width: 30%;
    }
    :nth-child(3) {
      justify-content: center;
      width: 45%;
    }
    :nth-child(4) {
      width: 25%;
    }

  }

  .staffItems {
    display: flex;
    text-align: center;
    border-radius: 0;
    border: 1px solid black;
    margin-top: 10px;

    .staffItem {
      height: 50px;
      display: inline-flex;
      align-items: center;
      justify-content: left;
      text-align: left;

    }

    :first-child {
      width: 10%;
      margin-left: 5%;
    }

    :nth-child(2) {
      width: 30%;
    }
    :nth-child(3) {
      justify-content: center;
      width: 45%;
    }
    :nth-child(4) {
      width: 25%;
    }

  }
}

</style>