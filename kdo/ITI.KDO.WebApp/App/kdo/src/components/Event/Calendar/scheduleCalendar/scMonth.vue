<template>
    <div class="schedule-calendar-month"
         @animationend="removeAnimation">
        <scDate v-for="(item, index) in days"
                 :date="item.date"
                 :type="item.type"
                 :data="data"
                 :index="index"
                 :key="index"
                 :draggedIndex="draggedIndex"
                 @highlight="highlight"></scDate>
    </div>
</template>
<script>
import { monthlyCalendar } from './utils'
import scDate from './scDate.vue'

export default {
    components: {
        scDate
    },
    props: {
        year: Number,
        month: Number,
        startWeek: Number,
        direction: String,
        data: Array
    },
    data() {
        return {
            viewTransition: 'sc-moveTo',
            draggedIndex: -1
        }
    },
    computed: {
        days() {
            return monthlyCalendar(this.year, this.month, this.startWeek)
        },
        animationClass() {
            return this.viewTransition + this.direction
        }
    },
    methods: {
        removeAnimation() {
            this.$el.classList.remove(this.animationClass)
        },
        addAnimation() {
            this.$el.classList.add(this.animationClass)
        },
        highlight(index) {
            this.draggedIndex = index
        }
    },
    watch: {
        year(val, old) {
            val !== old && this.addAnimation()
        },
        month(val, old) {
            val !== old && this.addAnimation()
        },
    },
}
</script>
<style lang="less">
@import './variables.less';

.schedule-calendar- {
    &month {
        position: absolute;
        top: @sc-week-height;
        left: 0;
        right: 0;
        bottom: 0;
        display: flex;
        flex-wrap: wrap; // transition: transform .3s ease-in-out;
    }
}

.sc-moveToLeft {
    animation: scMoveToLeft .3s both;
}

.sc-moveToRight {
    animation: scMoveToRight .3s both;
}

@keyframes scMoveToLeft {
    from {
        transform: translate3d(50%, 0, 0);
        visibility: visible;
    }

    to {
        transform: translate3d(0, 0, 0);
    }
}

@keyframes scMoveToRight {
    from {
        transform: translate3d(-50%, 0, 0);
        visibility: visible;
    }

    to {
        transform: translate3d(0, 0, 0);
    }
}
</style>
