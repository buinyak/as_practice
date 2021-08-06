<template>
  <div class="FileRedactor">
    <div class="buttons">
      <div v-if="chosedFile.name!=null" style="border-left:0" class="button">
        <input v-on:change="HandleFileUpload()" accept=".txt" type="file" id="file" ref="file"/>
      </div>
      <button @click="SaveFileServer()" v-if="chosedFile.name!=null" class="button">
        Сохранить на сервер
      </button>
    </div>
    <div class="bar">
      <div class="navigate">
          <div class="dirName">
            {{activeNav}}\
          </div>
          <div @click="chosedFile = file" class="fileName" v-for="file in dirs[this.activeNav]" v-bind:key="file.id">
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

export default {
  name: 'FileRedactor',
  props: ['activeNav'],
  data() {
    return {
      chosedFile: {},
      dirs: {}
    }
  },
  beforeMount() {
    this.GetAllFiles();
  },
  methods: {

    HandleFileUpload() {
      const reader = new FileReader();
      reader.onload = e => this.chosedFile.text = e.target.result;
      reader.readAsText(this.$refs.file.files[0]);
    },
    SaveFileServer() {
        axios.post('https://localhost:44390/txtfiles/update', {
              name: this.chosedFile.name,
              text: this.chosedFile.text,
              type: this.chosedFile.type
            }
        ).then(response => {
          if(response.data['error']){
            alert(response.data['error'])
          }
          console.log(response.data);
        }).catch((error) => {
          alert(error.data);
          console.log(error);
        })
    },

    GetAllFiles() {
      axios.get('https://localhost:44390/txtfiles/getAll'
      ).then(response => {
        this.dirs = response.data;
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
      border:0;
      border-left: 1px solid black;
      background-color:white;
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
