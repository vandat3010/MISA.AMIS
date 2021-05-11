<template>
  <div class="pagination">
    <div>
      Tổng số: <b>{{ totalRecord }}</b> bản ghi
    </div>
    <div class="pagination-right">
      <Autcomplete />
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
import Autcomplete from "../../components/Autocomplete.vue";
export default {
  components: {
    Autcomplete,
  },
  props: {
    /**
     * Prop tổng số trang.
     * CreatedBy: dbhuan (09/05/2021)
     */
    totalPages: {
      type: Number,
      default: 0,
    },

    /**
     * Tổng số bản ghi
     * CreatedBy: dbhuan (10/05/2021)
     */
    totalRecord: {
      type: Number,
      default: 0,
    },

    /**
     * Prop trang hiện tại
     * CreatedBy: dbhuan (09/05/2021)
     */
    pageIndex: {
      type: Number,
      default: 1,
    },
  },

  computed: {
    pageIndexs: function () {
      let ps = [];
      let start = this.pageIndex > 3 ? this.pageIndex - 1 : 2;
      let end =
        this.pageIndex < this.totalPages - 3 ? this.pageIndex + 1 : this.totalPages - 1;
      for (let i = start; i <= end; i++) ps.push(i);
      return ps;
    },
  },
};
</script>