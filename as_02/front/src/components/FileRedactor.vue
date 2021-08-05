<template>
  <div class="FileRedactor">
    <div class="buttons">
      <div v-if="chosedFile.name!=null" style="border-left-color:white" class="button">
        <input v-on:change="HandleFileUpload()" accept=".txt" type="file" id="file" ref="file"/>
      </div>
      <div @click="SaveFileServer()" v-if="chosedFile.name!=null" class="button">
        Сохранить на сервер
      </div>
    </div>
    <div class="bar">
      <div class="navigate">
          <div class="dirName">
            {{activeNav}}\
          </div>
          <div @click="chosedFile = file" class="fileName" v-for="file in dirs" v-bind:key="file.id">
            {{ file.name }}
          </div>
      </div>
      <textarea v-model="chosedFile.text" class="redactor">
      </textarea>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import Navigate from "@/components/Navigate";

export default {
  name: 'FileRedactor',
  props: ['activeNav'],
  comments: {Navigate},
  data() {
    return {
      chosedFile: {},
      dirs: null,
    }
  },
  mounted() {
    this.GetAllFiles();
  },
  methods: {

    HandleFileUpload() {
      const reader = new FileReader();
      reader.onload = e => this.chosedFile.text = e.target.result;
      reader.readAsText(this.$refs.file.files[0]);
    },
    ValidTxtFiles() {
      let matcher;
      switch (this.chosedFile.type) {
        case "digitFiles":
          matcher = /[^0-9]/;
          break;
        case "ruFiles":
          matcher = /[^а-яА-Я]/;
          break;
        case "engFiles":
          matcher = /[^a-zA-Z]/;
          break;
      }
      console.log(this.chosedFile.text.match(matcher));
      return this.chosedFile.text.match(matcher) == null;
    },
    SaveFileServer() {
      if (this.ValidTxtFiles()) {
        axios.post('https://localhost:44390/update', {
              name: this.chosedFile.name,
              text: this.chosedFile.text,
              type: this.chosedFile.type
            }
        ).then(response => {
          this.dirs = this.GetAllFiles();
          console.log(response.data);
        }).catch((error) => {
          console.log(error);
        })
      } else {
        alert("неверный формат");
      }

    },

    GetAllFiles() {
      axios.get('https://localhost:44390/getAll'
      ).then(response => {
        this.dirs = response.data[this.activeNav];
      }).catch((error) => {
        console.log(error);
      })
    },
    reload() {

    }
    ,
  }
}
</script>

<style scoped lang="scss">
.FileRedactor {
  font-family: 'Roboto', sans-serif;
  font-weight: 300;
  height: 400px;
  width: 800px;
  margin-left: auto;
  margin-right: auto;
  box-sizing: border-box;
  border-radius: 0;
  border: 1px solid black;

  .buttons {
    height: 10%;
    width: 100%;
    box-sizing: border-box;
    border-radius: 0;
    border-bottom: 1px solid black;
    display: flex;
    text-align: center;


    .button {
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

  .bar {
    display: flex;

    flex-direction: row;
    width: 100%;
    height: 90%;

    .navigate {
      width: 25%;
      text-align: left;
      display: flex;
      flex-direction: column;
      margin-top: 2%;

      .dirName {
        margin-left: 5%;
      }

      .fileName {
        cursor: pointer;
        margin-left: 12%;

        &:hover {
          color: blue;
        }
      }

    }

    .redactor {
      box-sizing: border-box;
      border-radius: 0;
      border-left: 1px solid black;
      width: 75%;
      padding-top: 2%;
      padding-left: 3%;
    }

    textarea {
      border: none;
      outline: none;
    }
  }
}
</style>
