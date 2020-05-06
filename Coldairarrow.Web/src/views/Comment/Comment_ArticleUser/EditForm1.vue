<template>
  <div>
    <a-card :bordered="false">
      <a-button type="primary" @click="getContent()">获取内容11</a-button>
      <a-button @click="setContent()">清空内容1</a-button>
      <a-button type="primary">查询1</a-button>
      <wang-editor v-model="content"></wang-editor>
    </a-card>
  </div>
</template>
<script>
import WangEditor from '@/components/WangEditor/WangEditor'
export default {
  components: {
    WangEditor
  },
  data () {
    return {
      content: ''
    }
  },
  methods: {
    getContent () {
      console.log(this.content)
    },
    setContent () {
      this.content = ''
    }
  }
}
</script>
