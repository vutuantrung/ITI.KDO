<template>
    <div class="schedule-calendar-body" >
        <scWeek :startWeek="startWeek"></scWeek>
        <scMonth  :year="year"
                  :month="month"
                  :startWeek="startWeek"
                  :direction="direction"
                  :data="data"></scMonth>
    </div>
</template>
<script>
import scWeek from './scWeek.vue';
import scMonth from './scMonth.vue';

export default {
    components: {
        scWeek,
        scMonth
    },
    props: {
        year: Number,
        month: Number,
        startWeek: Number,
        direction: String,
        data: Array
    }
}
</script>
<style lang="less">
@import './variables.less';

.schedule-calendar- {
    &body {
        position: relative;
        flex: 1;
        width: 100%;
        overflow: hidden;
        background: @sc-body-color;
    }
}
</style>
