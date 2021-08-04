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
        <div v-for="dir in dirs" v-bind:key="dir.id">
          {{"papka"}}
          <div @click="chosedFile = file" class="file" v-for="file in dir" v-bind:key="file.id">
            {{ file.name }}
          </div>
        </div>
      </div>
      <textarea v-model="chosedFile.text" class="redactor">
      </textarea>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'FileRedactor',
  props: {},
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
      reader.readAsText(this.$refs.file.files[0])

    },
    SaveFileServer() {
      axios.post('https://localhost:44390/update', {
            name: this.chosedFile.name,
            text: this.chosedFile.text
          }
      ).then(response => {
        this.dirs = this.GetAllFiles();
        console.log(response.data);
      }).catch((error) => {
        console.log(error);
      })
    },

    GetAllFiles() {
      axios.get('https://localhost:44390/getAll'
      ).then(response => {
        this.dirs = response.data;
        console.log(response.data)
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
      margin-top: 1%;

      .file {
        cursor: pointer;
        margin-left: 10%;

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
