﻿<template>
  <a-modal
    :title="title"
    width="40%"
    :visible="visible"
    :confirmLoading="loading"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-spin :spinning="loading">
      <a-form :form="form">
        <a-form-item label="文章标题" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['Tittle', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="作者" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['Author', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="文章类型" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['CategoryID', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="文章内容" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['Content', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="点赞数量" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['Supports', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="Banner图" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['BannerPic', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="状态" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['State', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="编码" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['Code', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="浏览总量" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['ViewCounts', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="发布日期" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['ReleaseTime', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="最后修改时间" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input
            v-decorator="['LastModifiedTime', { rules: [{ required: true, message: '必填' }] }]"
          />
        </a-form-item>
      </a-form>
    </a-spin>
  </a-modal>
</template>

<script>
export default {
  props: {
    parentObj: Object
  },
  data () {
    return {
      form: this.$form.createForm(this),
      labelCol: { xs: { span: 24 }, sm: { span: 7 } },
      wrapperCol: { xs: { span: 24 }, sm: { span: 13 } },
      visible: false,
      loading: false,
      formFields: {},
      entity: {},
      title: ''
    }
  },
  methods: {
    openForm (id, title) {
      // 参数赋值
      this.title = title || '编辑表单'
      this.loading = true

      // 组件初始化
      this.init()

      // 编辑赋值
      if (id) {
        this.$nextTick(() => {
          this.formFields = this.form.getFieldsValue()

          this.$http.post('/ArticleManage/Article_Info/GetTheData', { id: id }).then(resJson => {
            this.entity = resJson.Data
            var setData = {}
            Object.keys(this.formFields).forEach(item => {
              setData[item] = this.entity[item]
            })
            this.form.setFieldsValue(setData)
            this.loading = false
          })
        })
      } else {
        this.loading = false
      }
    },
    init () {
      this.entity = {}
      this.form.resetFields()
      this.visible = true
    },
    handleSubmit () {
      this.form.validateFields((errors, values) => {
        // 校验成功
        if (!errors) {
          this.entity = Object.assign(this.entity, this.form.getFieldsValue())

          this.loading = true
          this.$http.post('/ArticleManage/Article_Info/SaveData', this.entity).then(resJson => {
            this.loading = false

            if (resJson.Success) {
              this.$message.success('操作成功!')
              this.visible = false

              this.parentObj.getDataList()
            } else {
              this.$message.error(resJson.Msg)
            }
          })
        }
      })
    },
    handleCancel () {
      this.visible = false
    }
  }
}
</script>
