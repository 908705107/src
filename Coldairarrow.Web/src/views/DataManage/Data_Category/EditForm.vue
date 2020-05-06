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
        <a-form-item label="分类名称" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input
            v-decorator="[
              'CategoryName',
              { rules: [{ required: true, message: '必填' }] }
            ]"
          />
        </a-form-item>
        <a-form-item label="排序" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input
            v-decorator="[
              'Sort',
              { rules: [{ required: true, message: '必填' }] }
            ]"
          />
        </a-form-item>
        <a-form-item label="分类类型" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-select
            v-decorator="['Type', { rules: [{ required: true }]}]"
            allowClear
            placeholder="请选择分类类型"
          >
            <a-select-option
              v-for="item in ParentIdTreeData"
              :key="item.Id"
              :value="item.title"
            >{{ item.title }}</a-select-option>
          </a-select>
        </a-form-item>
        <!-- <a-form-item label="分类类型 " :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input
            v-decorator="[
              'Type',
              { rules: [{ required: true, message: '必填' }] }
            ]"
          />
        </a-form-item>-->
        <a-form-item label="上级分类" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-tree-select
            showSearch
            allowClear
            :dropdownStyle="{ maxHeight: '400px', overflow: 'auto' }"
            :treeData="AllTreeData"
            placeholder="请选择上级分类类型"
            treeDefaultExpandAll
            v-decorator="['Pid', { rules: [{ required: false }] }]"
          ></a-tree-select>
          <!-- <a-input
            v-decorator="[
              'Pid',
              { rules: [{ required: true, message: '必填' }] }
            ]"
          />-->
        </a-form-item>
        <a-form-item label="描述" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-input
            v-decorator="[
              'Remark',
              { rules: [{ required: true, message: '必填' }] }
            ]"
          />
        </a-form-item>
      </a-form>
    </a-spin>
  </a-modal>
</template>

<script>
export default {
  props: {
    afterSubmit: {
      type: Function,
      default: null
    }
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
      title: '',
      ParentIdTreeData: [],
      AllTreeData: []

    }
  },
  methods: {
    add () {
      this.entity = {}
      this.visible = true
      this.form.resetFields()
      this.init()
    },
    edit (id) {
      this.visible = true
      // 参数赋值
      this.title = '编辑表单'
      // 编辑赋值
      this.$nextTick(() => {
        this.formFields = this.form.getFieldsValue()

        this.$http
          .post('/DataManage/Data_Category/GetTheData', { id: id })
          .then(resJson => {
            this.entity = resJson.Data
            var setData = {}
            Object.keys(this.formFields).forEach(item => {
              setData[item] = this.entity[item]
            })
            this.form.setFieldsValue(setData)
            console.log(setData)
            this.init()
          })
      })
    },
    handleSubmit () {
      this.form.validateFields((errors, values) => {
        // 校验成功
        if (!errors) {
          this.entity = Object.assign(this.entity, this.form.getFieldsValue())
          console.log(this.entity)
          this.loading = true
          this.$http
            .post('/DataManage/Data_Category/SaveData', this.entity)
            .then(resJson => {
              this.loading = false

              if (resJson.Success) {
                this.$message.success('操作成功!')
                this.afterSubmit()
                this.visible = false
              } else {
                this.$message.error(resJson.Msg)
              }
            })
        }
      })
    },
    init () {
      // this.entity = {}
      // this.form.resetFields()
      this.$http.post('/DataManage/Data_Category/GetDataTreeList', { isall: true }).then(resJson => {
        if (resJson.Success) {
          this.AllTreeData = resJson.Data// 所有的数据
          var alldata = [...resJson.Data]// 深拷贝，解构赋值给alldata新变量
          console.log(alldata)
          // 去除children属性的数据--根节点数据
          this.ParentIdTreeData = alldata.map(a => {
            const { children, ...alldata } = a
            return {
              ...alldata
            }
          })
          console.log(this.ParentIdTreeData)
        }
      })
    },
    handleCancel () {
      this.visible = false
    }
  }
}
</script>
