<template>
  <div>
    <a-card>
      <a-input v-model="title" placeholder="请输入文章标题" allowClear />
    </a-card>
    <a-card :bordered="false">
      <a-button @click="getContent()">获取内容</a-button>
      <a-button @click="setContent()">清空内容</a-button>
      <wang-editor v-model="content"></wang-editor>
      <a-button-group>
        <a-button>Cancel</a-button>
        <a-button>存草稿</a-button>
        <a-button type="primary" @click="showDrawer">发布</a-button>
      </a-button-group>
      <!-- <a-row>
        <a-col :span="1" :offset="20">
          <a-button type="primary" icon="redo">存入草稿箱</a-button>
        </a-col>
        <a-col :offset="1">
          <a-button type="primary" icon="redo">发布</a-button>
        </a-col>

      </a-row>-->
    </a-card>
    <SetForm ref="show" :title="title"></SetForm>
  </div>
</template>
<script>
import WangEditor from '@/components/WangEditor/WangEditor'
import SetForm from './SetForm'
export default {
  components: {
    WangEditor,
    SetForm
  },
  data () {
    return {
      content: '',
      show: false,
      title: ''
    }
  },
  methods: {
    getContent () {
      console.log(this.content)
    },
    setContent () {
      this.content = ''
    },
    showDrawer () {
      this.$refs.show.showDrawer()
    },
    addResult () {
      // this.$http
      // .post('/Base_Manage/Base_Department/GetTreeDataList', {}

    }
  }
}
</script>
