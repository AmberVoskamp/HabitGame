import streamlit as st
import json
import pandas as pd
import plotly.express as px

# 1. Page Configuration
st.set_page_config(page_title="Game Analytics Pro", layout="wide")

st.title("🎮 Game Research Dashboard")
st.markdown("Upload your `.json` save files to analyze player behavior and boss progression.")

# 2. File Upload (Supports multiple files)
uploaded_files = st.file_uploader("Drag and drop JSON save files here", type=['json'], accept_multiple_files=True)

if uploaded_files:
    all_levels = []
    
    for uploaded_file in uploaded_files:
        file_name = uploaded_file.name
        try:
            data = json.load(uploaded_file)
            
            # Flatten the levelsData list and attach global run info
            for level in data.get("levelsData", []):
                level["source_file"] = file_name
                level["total_bosses_done"] = data.get("finishedAllBosses", False)
                all_levels.append(level)
        except Exception as e:
            st.error(f"Error loading {file_name}: {e}")

    if all_levels:
        df = pd.DataFrame(all_levels)

        # --- TOP LEVEL METRICS ---
        st.divider()
        m1, m2, m3, m4 = st.columns(4)
        m1.metric("Total Runs Analyzed", len(uploaded_files))
        m2.metric("Total Levels Played", len(df))
        m3.metric("Avg Boss Dmg Taken", f"{df['bossDamageTaken'].mean():.1f}")
        m4.metric("Successful Kills", int(df['killedTheBoss'].sum()))

        # --- MAIN TABS ---
        tab_overview, tab_walk, tab_raw = st.tabs(["📈 Run Overview", "🚶 Walk Data", "📑 Raw Table"])

        with tab_overview:
            col_a, col_b = st.columns(2)
            
            with col_a:
                st.subheader("Boss Health Progression")
                # Visualizes the boss health dropping over consecutive levels
                fig_health = px.line(df, x="index", y="bossHealthLeft", color="source_file",
                                     markers=True, title="Boss HP per Level Index")
                st.plotly_chart(fig_health, use_container_width=True)

            with col_b:
                st.subheader("Difficulty vs Damage")
                # Shows if higher spike difficulty actually correlates to more damage taken
                fig_spikes = px.scatter(df, x="spikeDificulty", y="spikesDamageTaken", 
                                        size="spikesDamageTaken", color="source_file",
                                        title="Spike Damage by Difficulty Level")
                st.plotly_chart(fig_spikes, use_container_width=True)

        with tab_walk:
            st.subheader("Player Movement Path")
            
            # Filters to help choose a specific level to inspect
            col_f1, col_f2 = st.columns(2)
            with col_f1:
                selected_file = st.selectbox("Pick a Save File", options=df['source_file'].unique())
            with col_f2:
                available_levels = df[df['source_file'] == selected_file]['index'].unique()
                selected_level = st.selectbox("Pick a Level Index", options=available_levels)

            # Extract walkData for the specific selection
            target_run = df[(df['source_file'] == selected_file) & (df['index'] == selected_level)]
            walk_list = target_run.iloc[0]['walkData']

            if walk_list and len(walk_list) > 0:
                # Convert the walk list to a dataframe and flatten the 'position' dict
                walk_df = pd.DataFrame(walk_list)
                walk_df = pd.concat([walk_df, walk_df['position'].apply(pd.Series)], axis=1)
                
                # Create the path map
                fig_walk = px.line(walk_df, x="x", y="y", text=None,
                                   hover_data=["timeLeft"], markers=True,
                                   title=f"Movement Path (File: {selected_file} | Level: {selected_level})")
                
                # Make the axes equal so the geometry isn't stretched
                fig_walk.update_yaxes(scaleanchor="x", scaleratio=1)
                st.plotly_chart(fig_walk, use_container_width=True)
                
                st.info(f"The player spent approximately {walk_df['timeLeft'].max() - walk_df['timeLeft'].min():.2f} seconds in this movement sequence.")
            else:
                st.warning("No walk data found for this specific level.")

        with tab_raw:
            st.subheader("Complete Dataset")
            st.dataframe(df, use_container_width=True)

else:
    st.info("👋 Welcome! Please upload one or more game save JSONs to begin the research.")