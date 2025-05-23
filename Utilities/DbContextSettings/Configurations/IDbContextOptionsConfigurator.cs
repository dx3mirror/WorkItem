﻿using Microsoft.EntityFrameworkCore;

namespace Utilities.DbContextSettings.Configurations
{
    /// <summary>
    /// Конфигуратор контекста базы данных.
    /// </summary>
    public interface IDbContextOptionsConfigurator<TContext>
        where TContext : DbContext
    {
        /// <summary>
        /// Настраивает контекст.
        /// </summary>
        /// <param name="options">настройки.</param>
        void Configure(DbContextOptionsBuilder<TContext> options);
    }
}
