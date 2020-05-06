<template>
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
        <a-form-item label="父ID" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['Pid', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="文件名称" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['Name', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="文件ID" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['FileID', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="分类" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['CategoryID', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="标签" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['Tag', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="0不是 1是文件夹" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['IsFolder', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="文件大小" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['Size', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="修改时间" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['UpdateTime', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="备注" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['Remark', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="文件名称" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['FileName', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="后缀" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['FileExtension', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="相对路径" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['FilePath', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="状态" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['State', { rules: [{ required: true, message: '必填' }] }]" />
        </a-form-item>
        <a-form-item label="别名" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input v-decorator="['FileAlias', { rules: [{ required: true, message: '必填' }] }]" />
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
  data() {
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
    openForm(id, title) {
      //参数赋值
      this.title = title || '编辑表单'
      this.loading = true

      //组件初始化
      this.init()

      //编辑赋值
      if (id) {
        this.$nextTick(() => {
          this.formFields = this.form.getFieldsValue()

          this.$http.post('/DataManage/Data_File/GetTheData', { id: id }).then(resJson => {
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
    init() {
      this.entity = {}
      this.form.resetFields()
      this.visible = true
    },
    handleSubmit() {
      this.form.validateFields((errors, values) => {
        //校验成功
        if (!errors) {
          this.entity = Object.assign(this.entity, this.form.getFieldsValue())

          this.loading = true
          this.$http.post('/DataManage/Data_File/SaveData', this.entity).then(resJson => {
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
    handleCancel() {
      this.visible = false
    }
  }
}
</script>
