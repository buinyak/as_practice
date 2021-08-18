<template>
  <div class="SkillsPage">
    <div clas="newSkill">
      <div class="newItems">
        <div class="newItem">ID</div>
        <input v-model="newSkill.name" placeholder="Название умения" class="newItem">
        <div @click="CreateSkill()" class="buttonAdd">Добавить</div>
      </div>
    </div>
    <div v-for="(skill,index) in skills" v-bind:key="skill.id">
      <div v-if="isUpdateSkill!=skill" class="skillItems">
        <div class="skillItem">{{ skill.id }}</div>
        <div class="skillItem">{{ skill.name }}</div>
        <div @click="isUpdateSkill=skill;updateSkill = skill;" class="buttonUpdate">Изменить</div>


        <div @click="DeleteSkill(skill,index)" class="buttonDelete">Удалить</div>
      </div>
      <div v-if="isUpdateSkill==skill" class="updateItems">
        <div class="updateItem">{{ skill.id }}</div>
        <input v-model="updateSkill.name" placeholder="Название умения" class="updateItem">
        <div @click="UpdateSkill();isUpdateSkill=null;" class="buttonConfirm">Сохранить</div>
        <div @click="isUpdateSkill=null" class="buttonBack">Отменить</div>
        <div class="updateItem"></div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
export default {
name: "SkillsPage",
  props: [],
  data() {
    return {
      skills: {},
      newSkill: {},
      updateSkill: {},
      isUpdateSkill: {}
    }
  },
  beforeMount() {
    this.GetAllSkills();
  },
  methods: {
    GetAllSkills() {
      axios.get('https://localhost:44390/skills/getall'
      ).then(response => {
        this.skills = response.data;
        console.log(response.data);
      }).catch((error) => {
        console.log(error);
      })
    },
    DeleteSkill(skill, index) {
      axios.delete('https://localhost:44390/skills/delete/' + skill.id
      ).then(response => {
        this.skills.splice(index,1);
        console.log(response.data);
      }).catch((error) => {
        console.log(error);
      })
    },
    CreateSkill() {
      if (!this.newSkill.name) {
        alert("Заполните все данные");
        return;
      }
      axios.post('https://localhost:44390/skills/create', this.newSkill
      ).then(response => {
        this.skills.push(response.data);
        console.log(response.data);
      }).catch((error) => {
        console.log(error);
      })
    },

    UpdateSkill() {
      console.log(this.updateSkill);
      if (!this.updateSkill.name) {
        alert("Заполните все данные");
        return;
      }
      axios.put('https://localhost:44390/skills/update', this.updateSkill
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
.SkillsPage {
  font-family: 'Roboto', sans-serif;
  margin-left: auto;
  margin-right: auto;
  box-sizing: border-box;
  width: 600px;
  font-size: 14pt;

  .newItems, .updateItems, .skillItems {
    height: 50px;
    display: flex;
    text-align: center;
    border-radius: 0;
    border: 1px solid black;
    margin-top: 10px;

    .newItem, .updateItem, .skillItem {
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
      width: 32%;
      margin-left: 2%;
      margin-right: 15%
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