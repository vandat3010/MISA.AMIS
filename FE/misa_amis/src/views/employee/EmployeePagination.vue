<template>
  <div class="pagination">
    <div>
      Tổng số: <b>{{ totalRecord }}</b> bản ghi
    </div>
    <div class="pagination-right">
      <select
        class="input"
        @change="$emit('onChangePageSize', $event.target.value)"
      >
        <option value="10">10 bản ghi trên trang</option>
        <option value="20">20 bản ghi trên trang</option>
        <option value="30">30 bản ghi trên trang</option>
        <option value="50">50 bản ghi trên trang</option>
        <option value="100">100 bản ghi trên trang</option>
        <option value="200">200 bản ghi trên trang</option>
      </select>
      <div class="pager">
        <div
          class="page"
          :class="{ disable: pageIndex == 1 }"
          @click.prevent="$emit('onChangePage', pageIndex - 1)"
        >
          Trước
        </div>

        <div
          class="page"
          :class="{ active: pageIndex == 1 }"
          @click.prevent="$emit('onChangePage', 1)"
        >
          1
        </div>

        <div v-if="pageIndex > 3" class="page disable">...</div>

        <div
          v-for="p in pageIndexs"
          :key="p"
          class="page"
          :class="{ active: pageIndex == p }"
          @click.prevent="$emit('onChangePage', p)"
        >
          {{ p }}
        </div>

        <div v-if="pageIndex < totalPages - 3" class="page disable">...</div>

        <div
          class="page"
          :class="{ active: pageIndex == totalPages }"
          @click.prevent="$emit('onChangePage', totalPages)"
        >
          {{ totalPages }}
        </div>

        <div
          class="page"
          :class="{ disable: pageIndex == totalPages }"
          @click.prevent="$emit('onChangePage', pageIndex + 1)"
        >
          Sau
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  //#region props
  props: {
    /**
     *  tổng số trang.
     * CreatedBy: NVDAT (09/05/2021)
     */
    totalPages: {
      type: Number,
      default: 0,
    },

    /**
     * Tổng số bản ghi
     * CreatedBy: NVDAT (10/05/2021)
     */
    totalRecord: {
      type: Number,
      default: 0,
    },

    /**
     * trang hiện tại
     * CreatedBy: NVDAT (09/05/2021)
     */
    pageIndex: {
      type: Number,
      default: 1,
    },
  },
  //#endregion

  //#region computed
  computed: {
    pageIndexs: function () {
      let ps = [];
      let start = this.pageIndex > 3 ? this.pageIndex - 1 : 2;
      let end =
        this.pageIndex < this.totalPages - 3
          ? this.pageIndex + 1
          : this.totalPages - 1;
      for (let i = start; i <= end; i++) ps.push(i);
      return ps;
    },
  },
  //#endregion
};
</script>