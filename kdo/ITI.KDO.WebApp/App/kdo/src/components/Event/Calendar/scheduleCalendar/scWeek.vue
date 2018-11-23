<template>
    <div class="schedule-calendar-week">
        <span v-for="wk in weeks"
              class="schedule-calendar-week-item">{{wk}}</span>
    </div>
</template>
<script>
export default {
    props: {
        startWeek: Number
    },
    data() {
        return {
            en: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday']
        }
    },
    computed: {
        weeks() {
            return this.startWeek === 0 ? this.en : this.en.concat(this.en.shift())
        }
    }
}
</script>
<style lang="less">
@import './variables.less';

.schedule-calendar- {
    &week {
        display: flex;
        height: @sc-week-height;
        line-height: @sc-week-height;
        background: @sc-primary-color
    }
    &week-item {
        flex: 1;
        text-align: center;
        color: @sc-body-color
    }
}
</style>
