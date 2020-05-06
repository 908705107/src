<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button
        v-if="hasPerm('Article_Info.Add')"
        type="primary"
        icon="plus"
        @click="hanldleAdd()"
      >新建</a-button>
      <a-button
        v-if="hasPerm('Article_Info.Delete')"
        type="primary"
        icon="minus"
        @click="handleDelete(selectedRowKeys)"
        :disabled="!hasSelected()"
        :loading="loading"
      >删除</a-button>
      <a-button type="primary" icon="redo" @click="getDataList()">刷新</a-button>
    </div>

    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="10">
          <a-col :md="4" :sm="24">
            <a-form-item label="查询类别">
              <a-select allowClear v-model="queryParam.condition">
                <a-select-option key="Tittle">文章标题</a-select-option>
                <a-select-option key="Author">作者</a-select-option>
                <a-select-option key="CategoryID">文章类型</a-select-option>
                <a-select-option key="Content">文章内容</a-select-option>
                <a-select-option key="BannerPic">Banner图</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item>
              <a-input v-model="queryParam.keyword" placeholder="关键字" />
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="24">
            <a-button type="primary" @click="getDataList">查询</a-button>
            <a-button style="margin-left: 8px" @click="() => (queryParam = {})">重置</a-button>
          </a-col>
        </a-row>
      </a-form>
    </div>

    <a-table
      ref="table"
      :columns="columns"
      :rowKey="row => row.Id"
      :dataSource="data"
      :pagination="pagination"
      :loading="loading"
      @change="handleTableChange"
      :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }"
      :bordered="true"
      size="small"
    >
      <span slot="action" slot-scope="text, record">
        <template>
          <template v-if="hasPerm('Article_Info.Edit')">
            <a @click="handleEdit(record.Id)">编辑</a>
            <a-divider type="vertical" />
            <a @click="handleDelete([record.Id])">删除</a>
          </template>
        </template>
      </span>
    </a-table>

    <edit-form ref="editForm" :parentObj="this"></edit-form>
  </a-card>
</template>

<script>
import EditForm from './EditForm'

const columns = [
  { title: '文章标题', dataIndex: 'Tittle', width: '10%' },
  { title: '作者', dataIndex: 'Author', width: '10%' },
  { title: '文章类型', dataIndex: 'CategoryID', width: '10%' },
  { title: '文章内容', dataIndex: 'Content', width: '10%' },
  { title: '点赞数量', dataIndex: 'Supports', width: '10%' },
  { title: 'Banner图', dataIndex: 'BannerPic', width: '10%' },
  { title: '状态', dataIndex: 'State', width: '10%' },
  { title: '编码', dataIndex: 'Code', width: '10%' },
  { title: '浏览总量', dataIndex: 'ViewCounts', width: '10%' },
  { title: '发布日期', dataIndex: 'ReleaseTime', width: '10%' },
  { title: '最后修改时间', dataIndex: 'LastModifiedTime', width: '10%' },
  { title: '操作', dataIndex: 'action', scopedSlots: { customRender: 'action' } }
]

export default {
  components: {
    EditForm
  },
  mounted () {
    this.getDataList()
  },
  data () {
    return {
      data: [],
      pagination: {
        current: 1,
        pageSize: 10,
        showTotal: (total, range) => `总数:${total} 当前:${range[0]}-${range[1]}`
      },
      filters: {},
      sorter: { field: 'Id', order: 'asc' },
      loading: false,
      columns,
      queryParam: {},
      selectedRowKeys: []
    }
  },
  methods: {
    handleTableChange (pagination, filters, sorter) {
      this.pagination = { ...pagination }
      this.filters = { ...filters }
      this.sorter = { ...sorter }
      this.getDataList()
    },
    getDataList () {
      this.selectedRowKeys = []

      this.loading = true
      this.$http
        .post('/ArticleManage/Article_Info/GetDataList', {
          PageIndex: this.pagination.current,
          PageRows: this.pagination.pageSize,
          SortField: this.sorter.field || 'Id',
          SortType: this.sorter.order,
          ...this.queryParam,
          ...this.filters
        })
        .then(resJson => {
          this.loading = false
          this.data = resJson.Data
          const pagination = { ...this.pagination }
          pagination.total = resJson.Total
          this.pagination = pagination
        })
    },
    onSelectChange (selectedRowKeys) {
      this.selectedRowKeys = selectedRowKeys
    },
    hasSelected () {
      return this.selectedRowKeys.length > 0
    },
    hanldleAdd () {
      this.$refs.editForm.openForm()
    },
    handleEdit (id) {
      this.$refs.editForm.openForm(id)
    },
    handleDelete (ids) {
      var thisObj = this
      this.$confirm({
        title: '确认删除吗?',
        onOk () {
          return new Promise((resolve, reject) => {
            thisObj.$http.post('/ArticleManage/Article_Info/DeleteData', { ids: JSON.stringify(ids) }).then(resJson => {
              resolve()

              if (resJson.Success) {
                thisObj.$message.success('操作成功!')

                thisObj.getDataList()
              } else {
                thisObj.$message.error(resJson.Msg)
              }
            })
          })
        }
      })
    }
  }
}
</script>
